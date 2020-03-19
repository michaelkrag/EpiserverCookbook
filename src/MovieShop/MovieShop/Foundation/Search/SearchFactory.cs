using MovieShop.Business.Services.Blobstore;
using NLPLib.NGrams;
using NLPLib.NGrams.Models;
using NLPLib.Search;
using NLPLib.Search.DocumentStores;
using NLPLib.Search.Models;
using NLPLib.Tokenizers;
using NLPLib.Vocabularys;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MovieShop.Foundation.Search
{
    public class SearchFactory
    {
        private IBlobRepository _blobRepository;
        private readonly IVocabulary _vocabulary;
        private IDocumentStorage _documentStorage;
        private readonly ITokinizer _tokinizer;

        public SearchFactory(IBlobRepository blobRepository, IVocabulary vocabulary, IDocumentStorage documentStorage, ITokinizer tokinizer)
        {
            _blobRepository = blobRepository;
            _vocabulary = vocabulary;
            _documentStorage = documentStorage;
            _tokinizer = tokinizer;
        }

        public IIrtRetSearch CreateSearch()
        {
            var searchItems = _blobRepository.Load<SearchExport>("Search");
            var search = new IrtRetSearch(_vocabulary, _documentStorage, _tokinizer);
            search.Import(searchItems);
            return search;
        }

        public IBiGram CreateBiGram()
        {
            var nGramData = _blobRepository.Load<ConcurrentDictionary<string, ContextWords>>("BiGram");
            var ngram = new NGram(5, new Sentencezer(new Tokinizer(new HashSet<string>() { "-", "\"", "(", ")", ":", ";", "," })));
            ngram.Impot(nGramData);
            return ngram;
        }

        public ITriGram CreateTriGram()
        {
            var nGramData = _blobRepository.Load<ConcurrentDictionary<string, ContextWords>>("TriGram");
            var ngram = new NGram(5, new Sentencezer(new Tokinizer(new HashSet<string>() { "-", "\"", "(", ")", ":", ";", "," })));
            ngram.Impot(nGramData);
            return ngram;
        }
    }
}