using System;
using System.Collections;

namespace SpellCheck
{
    internal class Spelling
    {
        private readonly SinglyLinkedList.SinglyLinkedList _list = new SinglyLinkedList.SinglyLinkedList();
        public Spelling(string[] reader)
        {
            foreach (var word in reader)
            {
                if(!word.StartsWith("%"))
                    _list.Add(word.ToLower());
            }
        }
        public bool CheckWord(string word)
        {
            return _list.Contains(word.ToLower());
        }
        public IEnumerable CheckText(string text)
        {
            var tokens = text.Split(new[] { ' ', '.', ',', ':', ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
                yield return new Word(token,  CheckWord(token));
        }
    }
}
