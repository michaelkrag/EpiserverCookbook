namespace NLPLib.Vocabularys.Models
{
    public class WordInfo
    {
        public string Word { get; set; }
        public int Occurs { get; set; }

        public override string ToString()
        {
            return $"{Word} : {Occurs}";
        }
    }
}