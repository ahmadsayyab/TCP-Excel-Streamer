using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    internal class Client
    {
        public static void ConnectAndReceiveData()
        {
            int port = 11000;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                Socket clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(ipEndPoint);
                Console.WriteLine("Client is connected");



                NetworkStream networkStream = new NetworkStream(clientSocket);
              
                using (StreamReader reader = new StreamReader(networkStream))
                {
                    string receivedData;
                    while (true)
                    {
                        receivedData = reader.ReadLine();
                        if (receivedData == null)
                        {
                            Console.WriteLine("Server disconnected.");
                            break;
                        }
                        Console.WriteLine(receivedData);

                       
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
