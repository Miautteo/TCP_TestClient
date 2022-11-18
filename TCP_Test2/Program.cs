using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Test2
{
    internal class Program
    {
        //Beginn-Methode
        static void Main(string[] args)
        {
            Console.Title = "TCP Client v0.1";

            while (true)
            {
                msg = Console.ReadLine();
                StartConnection();
            }

            

        }

        static int Port = 11000;
        static string Address = "2a02:908:f10:b440:d8fe:715f:f16:8ec9";

        static string msg = "Hallo ü.";

        static void StartConnection()
        {
            try
            {
                //TcpClient erstellt, der an $Adresse und $Port senden kann
                TcpClient client = new TcpClient(AddressFamily.InterNetworkV6);
                client.Connect(Address, Port);
                //TcpClient client = new TcpClient(Address, Port);

                //bytearray mit maximaler länge von 1024 wird erstellt und mit der nachricht gefüllt.
                byte[] sendData = new byte[1024];
                sendData = Encoding.UTF8.GetBytes(msg);

                //bytearray wird gesendet
                var stream = client.GetStream();
                var time = Stopwatch.StartNew();
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("Data sent");

                //es wird auf antwort gewartet
                var sr = new StreamReader(stream);
                string response = sr.ReadLine();
                time.Stop();
                var elapsed = time.Elapsed;
                Console.WriteLine(response + ", " + elapsed.TotalMilliseconds.ToString() + "ms elapsed");

                stream.Close();
                client.Close();

        }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: " + e.Message);
            }
}
    }
}
