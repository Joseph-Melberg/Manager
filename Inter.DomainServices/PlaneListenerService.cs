using Inter.Domain.PlaneModels;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using System;
namespace Inter.DomainServices
{
    public class PlaneListenerService : IPlaneListenerService
    {
        private readonly IPlaneListenerInfrastructureService _infra;
        public PlaneListenerService(IPlaneListenerInfrastructureService infrastructureService)
        {
            _infra = infrastructureService;
        }
        public void Decode(string message)
        {
            //Clean the messages
            message = CleanMessage(message);


            var hexList = message.Split(";\n*");
            //process the hexes
            foreach (var hex in hexList)
            {
                ParseHex(hex);
            }

        }
        private void ParseHex(string hexMessage)
        {
            var valid = ValidateLength(hexMessage.Length);
            if (!valid)
            {
                return;
            }
            var msg = StringToHexConverter(hexMessage);
            if (hexMessage.Length == (int)ModeMessageLengthEnum.ModeACMessageBytes * 2)
            {
                Console.WriteLine("I missed this A Message");
            }
            else
            {
                DecodeModeSMessage(msg);
            }


        }
        private void DecodeModeSMessage(byte[] binMessage)
        {
            var modeSMessage = new ModeSMessage();
            modeSMessage.msg = binMessage;
            modeSMessage.msgType = binMessage[0] >> 2;
            modeSMessage.msgbits = BitLengthByMessageType(modeSMessage.msgType);
            modeSMessage.crc = ExtractCrc1(modeSMessage);
            var crcVerified = CheckCheckSum(modeSMessage.msg, modeSMessage.msgbits);
            modeSMessage.errorBit = -1;
            modeSMessage.crcOk = modeSMessage.crc == crcVerified;
            if (!modeSMessage.crcOk)
            {
            //   Console.WriteLine($"Oopsie Poopsie, looks like something happened!");
            //    return;
            }
            Console.WriteLine("We gucci");
            modeSMessage.ca = binMessage[0] & 7;
            /* ICAO address */
            modeSMessage.aa1 = binMessage[1];
            modeSMessage.aa2 = binMessage[2];
            modeSMessage.aa3 = binMessage[3];
            /* DF 17 type (assuming this is a DF17, otherwise not used) */
            modeSMessage.metype = binMessage[4] >> 3;   /* Extended squitter message type. */
            modeSMessage.mesub = binMessage[4] & 7;     /* Extended squitter message subtype. */

            /* Fields for DF4,5,20,21 */
            modeSMessage.fs = binMessage[0] & 7;        /* Flight status for DF4,5,20,21 */
            modeSMessage.dr = binMessage[1] >> 3 & 31;  /* Request extraction of downlink request. */
            modeSMessage.um = ((binMessage[1] & 7) << 3) | /* Request extraction of downlink request. */
                      binMessage[2] >> 5;
            modeSMessage.identity = GenerateIdentity(binMessage);
            if(modeSMessage.msgType!= 6 && modeSMessage.msgType != 10)
            Console.WriteLine(modeSMessage.msgType);


            if((modeSMessage.msgType == 11 || modeSMessage.msgType == 17) &&  (modeSMessage.crcOk && modeSMessage.errorBit == -1))
            {
                UInt32 addr = ((uint)(modeSMessage.aa1 << 16))| ((uint)(modeSMessage.aa2 << 8))| (uint)(modeSMessage.aa3);
//                _infra.AddRecentICAOAddr(addr);
            }


        }


        private UInt32 ExtractCrc1(ModeSMessage message)
        {
            return ((UInt32)message.msg[(message.msgbits/8) - 3] << 16) |
               ((UInt32)message.msg[(message.msgbits/8) - 2] << 8) |
                (UInt32)message.msg[(message.msgbits/8) - 1];
        }
        private int GenerateIdentity(byte[] binMessage)
        {
            var a = ((binMessage[3] & 0x80) >> 5) |
                ((binMessage[2] & 0x02) >> 0) |
                ((binMessage[2] & 0x08) >> 3);
            var b = ((binMessage[3] & 0x02) << 1) |
                ((binMessage[3] & 0x08) >> 2) |
                ((binMessage[3] & 0x20) >> 5);
            var c = ((binMessage[2] & 0x01) << 2) |
                ((binMessage[2] & 0x04) >> 1) |
                ((binMessage[2] & 0x10) >> 4);
            var d = ((binMessage[3] & 0x01) << 2) |
                ((binMessage[3] & 0x04) >> 1) |
                ((binMessage[3] & 0x10) >> 4);
            return a * 1000 + b * 100 + c * 10 + b;
        }
        private int BitLengthByMessageType(int type)
        {
            switch (type)
            {
                case 16:
                case 17:
                case 19:
                case 20:
                case 21:
                    return (int)ModeMessageLengthEnum.ModeSLongBits;
                default:
                    return (int)ModeMessageLengthEnum.ModeSShortBits;
            }
        }
        private bool ValidateLength(int length)
        {
            switch (length)
            {
                case (int)ModeMessageLengthEnum.ModeACMessageBytes:
                case (int)ModeMessageLengthEnum.ModeSShortMessageBytes:
                case (int)ModeMessageLengthEnum.ModeSLongMessageBytes:
                    return true;
                default: return false;
            }
        }
        public byte[] StringToHexConverter(string message)
        {
            var result = new byte[message.Length / 2];
            for (var index = 0; index < message.Length; index += 2)
            {
                byte top = Convert.ToByte(message[index]);
                byte bottom = Convert.ToByte(message[index + 1]);
                byte liftedTop = (byte)(top << 4);
                result[index / 2] = (byte)(liftedTop | bottom);
            }
            return result;
        }
        public string CleanMessage(string message)
        {

            if (!message.StartsWith('*'))
            {
                message = message.Substring(Math.Max(message.IndexOf("*"), 0));
            }
            if (!message.EndsWith('\n'))
            {
                message = message.Remove(Math.Min(message.LastIndexOf('\n'), message.Length));
            }
            message = ClipEnds(message);
            return message;
        }
        private string ClipEnds(string message)
        {
            try
            {
                return message.Substring(1).Remove(message.Length - 2) ?? "What?";
            }
            catch (Exception ex)
            {
                return "Why";
            }
        }
        private UInt32 CheckCheckSum(byte[] msg, int bits)
        {
            UInt32 crc = 0;
            int offset = (bits == 112) ? 0 : (112 - 56);
            int j;

            for (j = 0; j < bits; j++)
            {
                int byte_ = j / 8;
                int bit = j % 8;
                int bitmask = 1 << (7 - bit);

                /* If bit is set, xor with corresponding table entry. */
                if ((msg[byte_] & bitmask) != 0)
                    crc ^= CheckSum[j + offset];
            }
            return crc; /* 24 bit checksum. */
        }
        readonly private UInt32[] CheckSum =
        {
0x3935ea, 0x1c9af5, 0xf1b77e, 0x78dbbf, 0xc397db, 0x9e31e9, 0xb0e2f0, 0x587178,
0x2c38bc, 0x161c5e, 0x0b0e2f, 0xfa7d13, 0x82c48d, 0xbe9842, 0x5f4c21, 0xd05c14,
0x682e0a, 0x341705, 0xe5f186, 0x72f8c3, 0xc68665, 0x9cb936, 0x4e5c9b, 0xd8d449,
0x939020, 0x49c810, 0x24e408, 0x127204, 0x093902, 0x049c81, 0xfdb444, 0x7eda22,
0x3f6d11, 0xe04c8c, 0x702646, 0x381323, 0xe3f395, 0x8e03ce, 0x4701e7, 0xdc7af7,
0x91c77f, 0xb719bb, 0xa476d9, 0xadc168, 0x56e0b4, 0x2b705a, 0x15b82d, 0xf52612,
0x7a9309, 0xc2b380, 0x6159c0, 0x30ace0, 0x185670, 0x0c2b38, 0x06159c, 0x030ace,
0x018567, 0xff38b7, 0x80665f, 0xbfc92b, 0xa01e91, 0xaff54c, 0x57faa6, 0x2bfd53,
0xea04ad, 0x8af852, 0x457c29, 0xdd4410, 0x6ea208, 0x375104, 0x1ba882, 0x0dd441,
0xf91024, 0x7c8812, 0x3e4409, 0xe0d800, 0x706c00, 0x383600, 0x1c1b00, 0x0e0d80,
0x0706c0, 0x038360, 0x01c1b0, 0x00e0d8, 0x00706c, 0x003836, 0x001c1b, 0xfff409,
0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000,
0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000,
0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000
};
    }
}