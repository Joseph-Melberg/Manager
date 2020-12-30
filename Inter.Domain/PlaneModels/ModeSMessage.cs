using System;

namespace Inter.Domain.PlaneModels
{
    public class ModeSMessage
    {
        //General Stuff
        public byte[] msg;
        public int msgbits;
        public int msgType;
        public bool crcOk;
        public UInt32 crc;
        public int errorBit;
        public int aa1;
        public int aa2;
        public int aa3;
        public int phaseCorrected;
        //DF 11
        public int ca;
        /* DF 17 */
        public int metype;                 /* Extended squitter message type. */
        public int mesub;                  /* Extended squitter message subtype. */
        public int heading_is_valid;
        public int heading;
        public int aircraft_type;
        public int fflag;                  /* 1 = Odd, 0 = Even CPR message. */
        public int tflag;                  /* UTC synchronized? */
        public int raw_latitude;           /* Non decoded latitude */
        public int raw_longitude;          /* Non decoded longitude */
        public char[] flight;             /* 8 chars flight number. */
        public int ew_dir;                 /* 0 = East, 1 = West. */
        public int ew_velocity;            /* E/W velocity. */
        public int ns_dir;                 /* 0 = North, 1 = South. */
        public int ns_velocity;            /* N/S velocity. */
        public int vert_rate_source;       /* Vertical rate source. */
        public int vert_rate_sign;         /* Vertical rate sign. */
        public int vert_rate;              /* Vertical rate. */
        public int velocity;               /* Computed from EW and NS velocity. */

        /* DF4, DF5, DF20, DF21 */
        public int fs;                     /* Flight status for DF4,5,20,21 */
        public int dr;                     /* Request extraction of downlink request. */
        public int um;                     /* Request extraction of downlink request. */
        public int identity;               /* 13 bits identity (Squawk). */

        /* Fields used by multiple message types. */
        public int altitude, unit;
    }
}