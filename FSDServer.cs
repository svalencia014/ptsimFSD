using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HelloWorld
{
    public class Hello
    {
        private static void Main()
        {
            Console.WriteLine("PTSIM FSD Server V0.1");
            Console.WriteLine("Copyright (c) 2022, PTSIM Project");
            Console.WriteLine("Staring Server. Please Wait...");
            Console.WriteLine("Press the enter key at any time to stop the server.");
            FSDServer.Server();
            Console.ReadKey();
        }
    }

    public class FSDServer
    {
        public static void Server()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 6809;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);
                server.Start();
                Console.WriteLine("Server Started Successfully");

                Byte[] bytes = new Byte[256];
                String data = null;

                while(true)
                {
                    Console.WriteLine("Waiting for connection");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connection Accepted");

                    data = null;

                    NetworkStream stream = client.GetStream();

                    string msg = "$DISERVER:CLIENT:VATSIM FSD V3.13:2656b9c15c380bf3438e8e";
                    byte[] buffer = System.Text.Encoding.ASCII.GetBytes(msg);

                    stream.Write(buffer);
                    Console.WriteLine("Sent: " + msg);
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
    }
}