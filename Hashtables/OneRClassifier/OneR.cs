using System;
using System.IO;
using System.Linq;
using Hashtable;

namespace OneRClassifier
{
    public class OneR
    {
        private class Item
        {
            public string Attribute { get; }
            public string Classification { get; }
            public int Frequency { get; set; }
            public Item(string attribute, string classification, int frequency = 1)
            {
                Classification = classification;
                Attribute = attribute;
                Frequency = frequency;
            }
            public override bool Equals(object obj) => obj is Item item && Attribute.Equals(item.Attribute) && Classification.Equals(item.Classification);
            public override int GetHashCode() => Classification.GetHashCode() + Attribute.GetHashCode();
        }
        private class ItemSet
        {
            public string Column { get; }
            public Hashtable<string, Item> Items { get; }
            public double ErrorRate { get; private set; }
            public ItemSet(string column)
            {
                Column = column;
                Items = new Hashtable<string, Item>(10);
            }
            public void AddItem(string attribute, string classification)
            {
                var key = attribute + "->" + classification;
                Item item = null;
                if (Items.ContainsKey(key))
                    item = Items[key];
                if (item == null)
                {
                    item = new Item(attribute, classification);
                    Items[key] = item;
                }
                else
                    item.Frequency++;
            }
            public void Process()
            {
                var result = from Item item in Items.Values()
                             orderby item.Attribute, item.Frequency descending
                             select item;
                var total = 0;
                var correct = 0;
                string attribute = null;
                foreach (var item in result)
                {
                    total += item.Frequency;
                    var key = item.Attribute + "->" + item.Classification;
                    if (attribute == null || attribute != item.Attribute)
                    {
                        attribute = item.Attribute;
                        correct += item.Frequency;
                    }
                    else Items.Remove(key);
                }
                ErrorRate = 100.0 - correct * 100.0 / total;
            }
            public override string ToString()
            {
                var s = Column + "\n";
                s += Items.Values().Aggregate(s, (current, value) => current + $"  {value.Attribute}->{value.Classification} : {value.Frequency}\n");
                if (!double.IsNaN(ErrorRate))
                    s += $"\n  ErrorRate: {ErrorRate} %\n";
                return s;
            }
        }
        public Hashtable<string, string> Solution { get; }
        public OneR()
        {
            Solution = new Hashtable<string, string>();
        }
        public void Build(TextReader r)
        {
            var list = new ArrayList<ItemSet>();
            var header = r.ReadLine()?.Split(';');
            if (header != null) foreach (var head in header) list.Add(new ItemSet(head));
            string line;
            while ((line = r.ReadLine()) != null)
            {
                var tokens = line.Split(';');
                var classfication = tokens[0];
                for (var i = 1; i < tokens.Length; i++)
                {
                    var attribute = tokens[i];
                    list[i].AddItem(attribute, classfication);
                }
            }
            r.Close();
            foreach (var items in list)
            {
                // vorher: Ausgabe alle Häufigkeitsverteilungen
                //Console.WriteLine(items);
                items.Process();
                // nacher: Ausgabe bester Häufigkeitsverteilungen
                //Console.WriteLine(items);
            }
            var result = from ItemSet items in list
                where !double.IsNaN(items.ErrorRate)
                orderby items.ErrorRate
                select items;
            var solution = result.First();
            foreach (var item in solution.Items.Values()) Solution.Add(item.Attribute, item.Classification);
            Console.WriteLine("".PadLeft(80, '_'));
            Console.WriteLine("Regeln: " + solution);
            list.Clear();
        }
        public string Classify(string value) => Solution[value];
    }
}