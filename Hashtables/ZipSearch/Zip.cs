using System;
using System.IO;
using Hashtable;

namespace ZipSearch
{
    public class Zip
    {
        private readonly Hashtable<int, Location> _locations = new Hashtable<int, Location>();
        public Zip(string file)
        {
            using (var r = new StreamReader(file))
            {
                var cols = r.ReadLine()?.Split(';');
                if (cols != null && cols.Length != 3)
                    throw new ArgumentException("More than 3 columns in zipcode file");

                string line;
                while ((line = r.ReadLine()) != null)
                {
                    cols = line.Split(new char[] { ';' });
                    var zipcode = int.Parse(cols[0]);
                    if (!_locations.ContainsKey(zipcode))
                    {
                        _locations.Add(zipcode, new Location
                        {
                            Zip = int.Parse(cols[0]),
                            State = cols[1],
                            City = cols[2]
                        });
                    }
                }
            }
        }
        public Location SearchLocation(int zipcode) => _locations.Contains(zipcode) ? _locations[zipcode] : null;
        public class Location
        {
            public int Zip { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public override string ToString() => Zip + " | " + State + " | " + City + "\n";
        }
    }
}