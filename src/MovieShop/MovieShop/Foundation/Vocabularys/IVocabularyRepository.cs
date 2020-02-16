using NLPLib.Tools.Wordbook;

namespace MovieShop.Foundation.Vocabularys
{
    public interface IVocabularyRepository
    {
        Vocabulary Get();

        void Set(Vocabulary vocabulary);
    }
}