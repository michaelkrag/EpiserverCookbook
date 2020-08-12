using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using Mediachase.Commerce.Catalog;
using MovieShop.Business.Services.Blobstore;
using MovieShop.Domain.Commerce.Products;
using MovieShop.Foundation.Extensions;
using MovieShop.Foundation.Search;
using Nest;
using NLPLib.NGrams;
using NLPLib.Search;
using NLPLib.Search.DocumentStores;
using NLPLib.Tokenizers;
using NLPLib.Vocabularys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MovieShop.Business.ScheduledJob
{
    [ScheduledPlugIn(DisplayName = "Movie indexer", GUID = "1BD8911A-B1DB-434B-A8D1-7027BD8AB88B")]
    public class MovieIndexerScheduledJob : ScheduledJobBase
    {
        private readonly ReferenceConverter _referenceConverter;
        private readonly IContentLoader _contentLoader;
        private readonly ElasticClient _elasticClient;

        public MovieIndexerScheduledJob(ReferenceConverter referenceConverter, IContentLoader contentLoader, ElasticClient elasticClient, IBlobRepository blobRepository, ISentencezer sentencezer)
        {
            _referenceConverter = referenceConverter;
            _contentLoader = contentLoader;
            _elasticClient = elasticClient;
        }

        public override string Execute()
        {
            var numberOfDocuments = 0;

            _elasticClient.Indices.Delete("movie");
            _elasticClient.Indices.Create("movie", x => x.Map<MovieDocument>(mm => mm.Properties(p => p.Keyword(t => t.Name(n => n.Genres)))));
            foreach (var contentData in _contentLoader.GetAllChildren<MovieProduct>(_referenceConverter.GetRootLink()))
            {
                if (contentData is ISearch movieProduct)
                {
                    var indexMove = new MovieDocument
                    {
                        Id = contentData.TheMovieDbId,
                        Adult = movieProduct.Adult,
                        BelongsToCollection = movieProduct.BelongsToCollection,
                        Casts = movieProduct.Casts?.ToList() ?? new List<Cast>(),
                        ContentLink = movieProduct.ContentLink,
                        Crews = movieProduct.Crews?.ToList() ?? new List<Crew>(),
                        Genres = movieProduct.Genres ?? "",
                        OrginalLang = movieProduct.OrginalLang ?? "",
                        OriginalTitle = movieProduct.OriginalTitle ?? "",
                        Overview = movieProduct.Overview ?? "",
                        Popularity = movieProduct.Popularity,
                        Poster = movieProduct.Poster,
                        ReleaseDate = movieProduct.ReleaseDate,
                        SpokenLanguages = movieProduct.SpokenLanguages ?? "",
                        Title = movieProduct.Title ?? "",
                        VoteAverage = movieProduct.VoteAverage,
                        VoteCount = movieProduct.VoteCount
                    };

                    var indexResponse = _elasticClient.IndexDocument(indexMove);
                    numberOfDocuments++;
                }
            }
            return $"Number of documents; {numberOfDocuments}";
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