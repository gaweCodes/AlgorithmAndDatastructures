using System;
using System.IO;
using BinarySearchTree;

namespace CompanySearch
{
    public class Companies
    {
        private readonly BinarySearchTree<string, Company> _companies = new BinarySearchTree<string, Company>();
        public Companies(TextReader r)
        {
            var tokens = r.ReadLine()?.Split(';');
            if (tokens != null && tokens.Length != 3) throw new ArgumentException("More than 3 columns in company file");

            string line;
            while ((line = r.ReadLine()) != null)
            {
                tokens = line.Split(';');
                _companies.Add(tokens[0], new Company
                {
                    Bezeichnung = tokens[0],
                    Branche = tokens[1],
                    Ort = tokens[2]
                });
            }
            r.Close();
        }
        public Company Read(string name) => _companies.Contains(name) ? _companies[name] : null;
    }
    public class Company
    {
        public string Bezeichnung { get; set; }
        public string Branche { get; set; }
        public string Ort { get; set; }
        public override string ToString() => $"Bezeichnung: {Bezeichnung}\tBranche: {Branche}\tOrt: {Ort}";
    }
}
