using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using static NPOI.HSSF.Util.HSSFColor;

namespace TCPServer
{
    internal class Server
    {
        public static void StartListening()
        {
            

            int port = 11000;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                Socket listnerSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listnerSocket.Bind(ipEndPoint);

                listnerSocket.Listen(10);

                Console.WriteLine("Waiting for connection...");

                Socket clientSocket = default(Socket);
                int counter = 0;


               
                while (true)
                {
                    counter++;
                    clientSocket = listnerSocket.Accept();
                    Console.WriteLine($"clients connected: {counter} ");

                    Thread userThread = new Thread(new ThreadStart(() => Server.SendDataToClient(clientSocket)));
                    userThread.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SendDataToClient(Socket client)
        {
            NetworkStream networkStream = new NetworkStream(client);
            using (StreamWriter writer = new StreamWriter(networkStream))
            {
                List<double> displacementData = LoadData.displacementData;
                List<double> loadData = LoadData.loadData;

                for (int i = 0; i < displacementData.Count; i++)
                {
                    double displacement = displacementData[i];
                    double load = loadData[i];

                    
                    string data = $"Displacement: {displacement} ## Load: {load}";
                    writer.WriteLine(data);
                    writer.Flush(); 

                    Thread.Sleep(2000); 
                }


            }

            client.Close();
        }

    }
}
 