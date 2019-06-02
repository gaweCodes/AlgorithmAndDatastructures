namespace SpellCheck
{
    internal class Word
    {
        private readonly string _word;
        public bool IsCorrect { get; set; }
        public Word(string word, bool isCorrect)
        {
            this._word = word;
            IsCorrect = isCorrect;
        }
        public override string ToString()
        {
            return _word;
        }
    }
}
