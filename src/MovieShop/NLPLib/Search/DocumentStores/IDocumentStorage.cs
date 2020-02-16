namespace NLPLib.Search.DocumentStores
{
    public interface IDocumentStorage
    {
        int Insert<TObj>(int documentId, TObj obj);

        TObj Get<TObj>(int id);
    }
}