using System;

namespace ZipSearch
{
    internal class Program
    {
        private static void Main()
        {
            var zip = new Zip("ZIPCodes.csv");
            int zipcode;
            do
            {
                Console.WriteLine("ZIPCode eingeben, z.B. 35036: ");
                if(!int.TryParse(Console.ReadLine(), out zipcode))
                    return;
                Zip.Location location = zip.SearchLocation(zipcode);
                if (location != null) Console.WriteLine(location);
                else Console.WriteLine("Nicht gefunden!");
            } while (zipcode != 0);
            Console.ReadLine();
        }
    }
}
