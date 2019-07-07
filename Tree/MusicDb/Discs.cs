using System;
using System.IO;
using System.Text;
using RedBlackTree;

namespace MusicDb
{
    [Serializable]
    public class Discs
    {
        private readonly RedBlackTree<string, Disc> _discTitles;
        private readonly string _musicFolder;
        public Discs(string musicFolder)
        {
            _musicFolder = musicFolder;
            _discTitles = new RedBlackTree<string, Disc>();
        }
        public void Load()
        {
            foreach (var file in Directory.EnumerateFiles(_musicFolder, "*.", SearchOption.AllDirectories)) ReadDiscFile(file);
        }
        public Disc Read(string discId) => _discTitles.Contains(discId) ? _discTitles[discId] : null;
        private void ReadDiscFile(string file)
        {
            using (var reader = new StreamReader(file, Encoding.Default))
            {
                string line;
                string discTitle = null, genre = null, discId = null;
                var year = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line[0] == '#') continue;
                    var index = line.IndexOf('=');
                    var key = line.Substring(0, index);
                    var value = line.Substring(index + 1);
                    switch (key)
                    {
                        case "DISCID":
                            discId = value;
                            break;
                        case "DTITLE":
                            discTitle = value;
                            break;
                        case "DGENRE":
                            genre = value;
                            break;
                        case "DYEAR":
                            int.TryParse(value, out year);
                            break;
                    }
                }
                if (discId != null) _discTitles.Add(discId, new Disc(discId, discTitle, year, genre));
            }
        }
    }
    [Serializable]
    public class Disc
    {
        public Disc(string discId, string discTitle, int year, string genre)
        {
            DiscId = discId;
            DiscTitle = discTitle;
            Year = year;
            Genre = genre;
        }
        public string DiscId { get; set; }
        public string DiscTitle { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public override string ToString() => $"{DiscId} | {DiscTitle} | {Year} | {Genre}";
    }
}
