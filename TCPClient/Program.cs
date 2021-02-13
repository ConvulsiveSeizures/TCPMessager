using System;
using System.Net.Sockets;
using System.Text;

namespace TcpClientApp
{
    class Program
    {


        static void Main(string[] args)
        {
            const string IP = "127.0.0.1";
            const int PORT = 8888;

            Console.WriteLine("Etner username: ");
            string userName = Console.ReadLine();

            TcpClient client = null;
            try 
            {

                client = new TcpClient(IP, PORT);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Console.WriteLine($"{userName}: ");
                    string message = Console.ReadLine();
                    message = String.Format("{0}: {1}", userName, message);

                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    data = new byte[256];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;

                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));

                    } while (stream.DataAvailable);

                    message = builder.ToString();
                    Console.WriteLine($"Server: {message}");

                }
                
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}