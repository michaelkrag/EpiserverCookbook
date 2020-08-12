using SuggestionApi.NLP.Vocabularys;
using System;

namespace SuggestionApi.Services
{
    public class Index : IDisposable
    {
        public readonly DocumentStore DocumentStore;

        public readonly IVocabulary Vocabulary;

        public Index(DocumentStore documentStore, IVocabulary vocabulary)
        {
            DocumentStore = documentStore;
            Vocabulary = vocabulary;
        }

        public void Dispose()
        {
            DocumentStore.Dispose();
        }
    }
}