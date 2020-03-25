namespace NLPLib.Search.Models
{
    public class DocumentScore
    {
        public int DocumentId { get; set; }
        public double Score { get; set; }

        public override string ToString()
        {
            return $"id: {DocumentId}  , score: {Score}";
        }
    }
}