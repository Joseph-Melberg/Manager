using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Inter.DomainServices.Core;

namespace Inter.PlaneListenerAppService.Application
{
    public class PlaneProcessor
    {
        private readonly IPlaneListenerService _service;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public PlaneProcessor(IPlaneListenerService service)
        {
            _service = service;
        }

        public void Run()
        {

            string ip = "127.0.0.1";
            int port = 30004;

            try
            {
                //TcpClient client = server.AcceptTcpClient();
                var socket = ConnectSocket("0.0.0.0", port);
                while (true)
                {
                    allDone.Reset();
                    Console.WriteLine("Waiting for connection");
                    socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);

                    allDone.WaitOne();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            try
            {
                while (true)
                {

                    StateObject state = new StateObject();
                    state.workSocket = handler;
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                }
            }
            catch (Exception ex)
            {
                handler.Close();
                Console.WriteLine(ex.ToString());
            }
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                content = state.Remnant + state.sb.ToString();
                var isNewl = content.EndsWith("\n");
                var isCol = content.EndsWith(";");
                if (true)
                { // All the data has been read from the // client. Display it on the console.  
                    //Console.WriteLine(content.Remove(content.LastIndexOf('\n')));
                    
                    //Console.WriteLine("Read {0} bytes from socket. \n",
                        //content.Length);
                    _service.Decode(content);
                   
                                        
                    // Not all data received. Get more.  
                  //  handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    //new AsyncCallback(ReadCallback), state);
                }
            }
        }
        private Socket ConnectSocket(string address, int port)
        {

            Socket s = null;

            try
            {

                s = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
            }

            var ipaddress = IPAddress.Parse(address);
            var localEndPoint = new IPEndPoint(ipaddress, port);


            s.Bind(localEndPoint);
            s.Listen(100);

            return s;
        }
        public class StateObject
        {
            public string Remnant = "";
            // Size of receive buffer.  
            public const int BufferSize = 1024; 

            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];

            // Received data string.
            public StringBuilder sb = new StringBuilder();

            // Client socket.
            public Socket workSocket = null;
        }
    }
}