using System;
using System.IO;
using System.Collections;
using BinarySearchTree;

namespace SpellCheckBinaryTree
{
    public class Spelling
    {
        private readonly BinarySearchTree<string, string> _tree;
        public class Word
        {
            private readonly string _word;
            public bool IsCorrect { get; }
            public Word(string word, bool isCorrect)
            {
                _word = word;
                IsCorrect = isCorrect;
            }
            public override string ToString() => _word;
        }
        public Spelling(TextReader reader)
        {
            _tree = new BinarySearchTree<string, string>();
            string word;
            while ((word = reader.ReadLine()) != null)
                if (!word.StartsWith("%")) _tree.Add(word.ToLower(), null);
            reader.Close();
        }
        public bool CheckWord(string word) => _tree.Contains(word.ToLower());
        public IEnumerable CheckText(string text)
        {
            var tokens = text.Split(new[] { ' ', '.', ',', ':', ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string token in tokens)
                yield return new Word(token, _tree.Contains(token.ToLower()));
        }
    }
}
