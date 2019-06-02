using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WebServer
{
    internal class HttpServer
    {
        TcpListener _listener;
        public int Port { get; }
        public string LocalPath { get; }
        public HttpServer(int port, string localPath)
        {
            Port = port;
            LocalPath = localPath;
        }
        public void Start()
        {
            _listener = new TcpListener(IPAddress.Loopback, Port);
            _listener.Start();
            var requests = new Requests();
            while (true)
            {
                var c = _listener.AcceptTcpClient();
                requests.Add(c);
                if (!_listener.Pending()) requests.Process(LocalPath);
            }
        }
        private class Requests
        {
            readonly Queue.Queue<TcpClient> queue = new Queue.Queue<TcpClient>();
            public void Add(TcpClient c)
            {
                queue.Enqueue(c);
            }
            public void Process(string path)
            {
                Console.WriteLine("Queue: " + queue.Count);
                while (queue.Count > 0)
                {
                    try
                    {
                        var r = new Request(queue.Dequeue(), path);
                        Console.WriteLine("  " + r.LocalUrl);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }
        private class Request
        {
            private readonly TcpClient _c;
            private readonly string _localPath;
            public string LocalUrl { get; private set; }
            public string Verb { get; private set; }
            public string HttpVersion { get; private set; }
            public List<string> Header { get; private set; }
            public Request(TcpClient c, string localPath)
            {
                _c = c;
                _localPath = localPath;
                Header = new List<string>();
                Process();
            }
            private void Process()
            {
                var r = new StreamReader(_c.GetStream());
                var w = new StreamWriter(_c.GetStream());
                try
                {
                    HandleRequest(r, w);
                }
                catch (Exception)
                {
                    Failure(w);
                    throw;
                }
                finally
                {
                    w.Close();
                    r.Close();
                    _c.Close();
                }
            }
            private void HandleRequest(StreamReader r, StreamWriter w)
            {
                var request = r.ReadLine();
                if (request == null) return;
                var tokens = request.Split(' ');
                Verb = tokens[0].ToUpper();
                LocalUrl = tokens[1];
                HttpVersion = tokens[2];
                ReadHeader(r);
                var file = Path.Combine(Path.GetFullPath(_localPath), LocalUrl.TrimStart('/'));
                var text = File.ReadAllText(file);
                Success(w, Path.GetExtension(LocalUrl).TrimStart('.'));
                w.Write(text);
            }
            private void ReadHeader(TextReader r)
            {
                string attribute;
                while ((attribute = r.ReadLine()) != null)
                {
                    if (attribute.Equals("")) break;
                    Header.Add(attribute);
                }
            }
            private static void Success(TextWriter stream, string ext)
            {
                stream.WriteLine("HTTP/1.0 200 OK");
                stream.WriteLine("Content-Type: text/" + ext);
                stream.WriteLine("Connection: close");
                stream.WriteLine("");
            }
            private static void Failure(TextWriter stream)
            {
                stream.WriteLine("HTTP/1.0 404 not found");
                stream.WriteLine("Connection: close");
                stream.WriteLine("");
            }
        }
    }
}
