using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MusicDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException("Aufruf: MusicDB <folder>");
            var discs = new Discs(args[0]);
            var fileDb = Path.Combine(args[0], "musicDB.bin");
            if (!File.Exists(fileDb))
                discs.Load();
            else
            {
                using (Stream stream = File.OpenRead(fileDb))
                {
                    var deserializer = new BinaryFormatter();
                    discs = (Discs)deserializer.Deserialize(stream);
                }
            }
            Console.Write("DiscId eingeben, z.B. 2d0bde04: ");
            string discId;
            while ((discId = Console.ReadLine()) != string.Empty)
            {
                if (string.IsNullOrEmpty(discId)) break;
                var title = discs.Read(discId);
                Console.WriteLine(title == null ? "Titel nicht gefunden!" : $"{title}\n");
            }
            using (var stream = File.Create(fileDb))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, discs);
            }
        }
    }
}
