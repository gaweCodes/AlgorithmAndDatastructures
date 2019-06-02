using System;

namespace WebServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var port = int.Parse(args[0]);
            var localPath = args[1];
            Console.WriteLine($"Starting 'Very Simple Webserver' at port {port}, path \"{localPath}\"");
            new HttpServer(port, localPath).Start();
        }
    }
}
