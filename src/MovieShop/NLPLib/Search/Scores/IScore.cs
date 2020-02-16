namespace NLPLib.Search.Scores
{
    public interface IScore
    {
        double Score(int documentId, int numberOfDocumentsWithTerm, int termHitsInDocument, int numberOfTermsForDoc);
    }
}