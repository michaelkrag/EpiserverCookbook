using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using Mediachase.Commerce.Catalog;
using MovieShop.Business.Services.Blobstore;
using MovieShop.Features.Catalog.Models;
using NLPLib.Tokenizer;
using NLPLib.Tools.Wordbook;
using System.Collections.Generic;
using System.Linq;

namespace MovieShop.Business.ScheduledJob
{
    [ScheduledPlugIn(DisplayName = "Create vocabulary", GUID = "B9A03416-0FA9-44CD-A26C-5C4B698C466F")]
    public class VocabularyScheduledJob : ScheduledJobBase
    {
        private readonly ReferenceConverter _referenceConverter;
        private readonly IContentLoader _contentLoader;
        private readonly IBlobRepository _blobRepository;
        private static Dictionary<string, string> abbreviations = new Dictionary<string, string>() { { "u.s.", "United States" }, { "dr.", "doctor" }, { "jr.", " junior" }, { "mr.", "Mister" }, { "l.a.", "Los Angeles" } };
        private static HashSet<string> stopwords = new HashSet<string>() { "-", };

        public VocabularyScheduledJob(ReferenceConverter referenceConverter, IContentLoader contentLoader, IBlobRepository blobRepository)
        {
            _referenceConverter = referenceConverter;
            _contentLoader = contentLoader;
            _blobRepository = blobRepository;
        }

        public override string Execute()
        {
            var rootLink = _referenceConverter.GetRootLink();
            var catalogRef = _contentLoader.GetChildren<CatalogContentBase>(rootLink).First();

            var nodeQueue = new Queue<CatalogContentBase>(new List<CatalogContentBase>() { catalogRef });

            var tf = new TokenizeFactory(abbreviations, stopwords);
            var sentencesTokenize = tf.CreateSentencesTokenize();
            var wordTokenize = tf.CreateWordTokenize();
            var vocabulary = new Vocabulary();
            while (nodeQueue.Any())
            {
                var contentData = nodeQueue.Dequeue();

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

                var children = _contentLoader.GetChildren<CatalogContentBase>(contentData.ContentLink);

                foreach (var child in children)
                {
                    nodeQueue.Enqueue(child);
                }
            }
            _blobRepository.Save("Vocabulary", vocabulary);
            return $"Found {vocabulary.Count()} words";
        }
    }
}