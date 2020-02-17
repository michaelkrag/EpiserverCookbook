using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using Mediachase.Commerce.Catalog;
using MovieShop.Business.Services.Blobstore;
using MovieShop.Foundation.Extensions;
using MovieShop.Foundation.Search;
using NLPLib.Search;
using NLPLib.Search.DocumentStores;
using NLPLib.Tokenizers;
using NLPLib.Vocabularys;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MovieShop.Business.ScheduledJob
{
    [ScheduledPlugIn(DisplayName = "Create vocabulary", GUID = "B9A03416-0FA9-44CD-A26C-5C4B698C466F")]
    public class VocabularyScheduledJob : ScheduledJobBase
    {
        private readonly ReferenceConverter _referenceConverter;
        private readonly IContentLoader _contentLoader;
        private readonly IBlobRepository _blobRepository;
        private static Dictionary<string, string> abbreviations = new Dictionary<string, string>() { { "u.s.", "United States" }, { "dr.", "doctor" }, { "jr.", " junior" }, { "mr.", "mister" }, { "l.a.", "Los Angeles" } };
        private static HashSet<string> stopwords = new HashSet<string>() { "-", "!", "?", ".", "\"", "(", ")", ":", ";", "," };

        public VocabularyScheduledJob(ReferenceConverter referenceConverter, IContentLoader contentLoader, IBlobRepository blobRepository)
        {
            _referenceConverter = referenceConverter;
            _contentLoader = contentLoader;
            _blobRepository = blobRepository;
        }

        public override string Execute()
        {
            var tokinizer = new Tokinizer(stopwords);
            var documentStore = new DocumentStorageMemory();
            var vocabulary = new Vocabulary();
            var search = new IrtRetSearch(vocabulary, documentStore, tokinizer);
            var numberOfDocuments = 0;
            foreach (var contentData in _contentLoader.GetAllChildren<CatalogContentBase>(_referenceConverter.GetRootLink()))
            {
                if (contentData is ISearch movieProduct)
                {
                    search.Indexing<ISearch>(contentData.ContentLink.ID, movieProduct);
                    numberOfDocuments++;
                    Debug.WriteLine(movieProduct.Title);
                }
            }

            var vocabularyEntrys = vocabulary.Export();
            _blobRepository.Save("Vocabulary", vocabularyEntrys);

            return $"Number of documents; {numberOfDocuments}, number of words {vocabularyEntrys.Count()}";
        }

        /*
        public string oldExecute()
        {
            var tf = new TokenizeFactory(abbreviations, stopwords);
            var sentencesTokenize = tf.CreateSentencesTokenize();
            var wordTokenize = tf.CreateWordTokenize();
            var vocabulary = new Vocabulary();

            foreach (var contentData in _contentLoader.GetAllChildren<CatalogContentBase>(_referenceConverter.GetRootLink()))
            {
                if (contentData is MovieProduct movieProduct)
                {
                    var title = new List<string>() { movieProduct.Title, movieProduct.Summery };
                    foreach (var item in title)
                    {
                        foreach (var sentence in sentencesTokenize.GetSentences(item))
                        {
                            var words = wordTokenize.GetTokens(sentence);
                            vocabulary.Insert(words);
                        }
                    }
                }
            }
            _blobRepository.Save("Vocabulary", vocabulary);
            return $"Found {vocabulary.Count()} words";
        }*/
    }
}