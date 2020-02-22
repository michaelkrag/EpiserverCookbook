namespace NLPLib.Search.DocumentStores
{
    public interface IDocumentStorage
    {
        int Insert<TObj>(int documentId, TObj obj) where TObj : class;

        TObj Get<TObj>(int id) where TObj : class;
    }
}