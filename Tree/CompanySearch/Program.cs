using System;
using System.IO;
using System.Text;

namespace CompanySearch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException("Aufruf: CompanySearch <file.csv>");
            var companies = new Companies(new StreamReader(args[0], Encoding.Default));
            while (true)
            {
                Console.Write("Unternehmensbezeichnung eingeben: ");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                    break;

                var company = companies.Read(name);
                Console.WriteLine(company == null ? "Unternehmen nicht gefunden!" : company + "\n");
            }
    }
}
