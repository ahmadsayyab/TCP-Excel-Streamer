using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoadData.ReadExcelFile();
            Server.StartListening();
        }
    }
}
