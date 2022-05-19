using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

//REFER TO FLOW.MD TO WORK ON SERVER

namespace PTSIM.FSDServer
{
    public class FSDServer
    {
        public static void Start()
        {
            Console.WriteLine("PTSIM FSD Server Version 1.0");
            Thread.Sleep(50);
            Console.WriteLine("Copyright (c) 2022, PTSIM Project");
            Thread.Sleep(50);
            Console.WriteLine("Starting Server. Please Wait...");
            Thread.Sleep(50);
            Server();

        }
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
                Thread.Sleep(50);
                
                Byte[] bytes = new Byte[256];
                String data = null;

                while(true)
                {
                    Console.WriteLine("Waiting for connection");
                    TcpClient client = server.AcceptTcpClient();
                    Thread.Sleep(50);
                    Console.WriteLine("Connection Accepted");

                    data = null;

                    NetworkStream stream = client.GetStream(); //Grab initial stream

                    //22 Character hexadecimal generator here
                    String token = Generate.HexString(22).ToLower();
                    Console.WriteLine(token); //Print hexadecimal string here for debugging. 
                    
                    string msg = $"$DISERVER:CLIENT:VATSIM FSD V3.4h:{token}"; //Server Identifier Stream
                    byte[] buffer = Encoding.UTF8.GetBytes(msg);

                    stream.Write(buffer);
                    Console.WriteLine("Sent: " + msg);

                    int i;

                    while((i = stream.Read(bytes, 0, bytes.Length)) !=0) //Client Identifier Stream
                    {
                        data = Encoding.UTF8.GetString(bytes, 0, i);
                        Console.WriteLine("Recieved: " + data);

                        msg = "#TMserver:(callsign):Welcome to the PTSIM network!"; //Send Message of the Day
                        buffer = Encoding.UTF8.GetBytes(msg);

                        stream.Write(buffer);
                        Console.WriteLine("Sent: " + msg);

                        break;

                    }
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("Error: " + e);
            }
        }

    }

    public class Generate
    { 
        public static string HexString(int digits)
        {
            Random random = new Random();

            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits %2 == 0)
            {
                return result;
            }
            return result + random.Next(16).ToString("X");
        }
    }
}