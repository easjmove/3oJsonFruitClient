using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using JsonFruit;

namespace JsonFruitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fruit client:");
            TcpClient socket = new TcpClient("localhost", 10002);

            Console.WriteLine("Enter type of fruit:");
            string typeOfFruit = Console.ReadLine();

            Fruit myFruit = new(typeOfFruit);
            string serializedFruit = JsonSerializer.Serialize(myFruit);

            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            writer.WriteLine(serializedFruit);
            writer.Flush();
            string response = reader.ReadLine();
            Console.WriteLine("Response: " + response);

            socket.Close();
        }
    }
}
