using EPiServer.ServiceLocation;
using MovieShop.Business.Services.Blobstore;
using MovieShop.Business.Services.Search;
using CommonLib.Extensions;
using System;
using System.Linq;
using MovieShop.Infrastructure.DependencyInjection.Extensions;
using MovieShop.Foundation.Search;
using NLPLib.TernaryTree;
using NLPLib.Search;
using NLPLib.Vocabularys;
using System.Collections.Generic;
using NLPLib.Vocabularys.Models;
using NLPLib.Search.DocumentStores;
using MovieShop.Adapters.DocumentStore;
using NLPLib.Tokenizers;
using NLPLib.NGrams;

namespace MovieShop.Infrastructure.DependencyInjection
{
    public class DependencyInjectionConfig
    {
        public static void Setup(IServiceConfigurationProvider container)
        {
            SetupDefaultConvention(container);
            SetupFeatureModules(container);
            SetupExternal(container);
        }

        private static void SetupExternal(IServiceConfigurationProvider container)
        {
            container.AddTransient<IBlobFilenameRepository, BlobFilenameRepository>();

            container.AddTransient<IBlobRepository, BlobRepository>();

            container.AddSingleton<IVocabulary>(x => CreateVocabulary(x.GetInstance<IBlobRepository>()));

            container.AddSingleton<ITernaryTreeFactory, TernaryTreeFactory>();
            container.AddSingleton<ITernarySearch>(x => x.GetInstance<ITernaryTreeFactory>().GenerateTree());
            container.AddSingleton<ITokinizer>(new Tokinizer(new HashSet<string>() { "-", "!", "?", ".", "\"", "(", ")", ":", ";", "," }));
            container.AddSingleton<IDocumentStorage, DocumentStore>();
            container.AddSingleton<SearchFactory, SearchFactory>();
            container.AddSingleton<IIrtRetSearch>(x => x.GetInstance<SearchFactory>().CreateSearch());
            container.AddSingleton<IBiGram>(x => x.GetInstance<SearchFactory>().CreateBiGram());
            container.AddSingleton<ITriGram>(x => x.GetInstance<SearchFactory>().CreateTriGram());
            container.AddSingleton<ISentencezer>(new Sentencezer(new Tokinizer(new HashSet<string>() { "-", "\"", "(", ")", ":", ";", "," })));
        }

        private static IVocabulary CreateVocabulary(IBlobRepository blobRepository)
        {
            var vocabularyItems = blobRepository.Load<List<VocabularyItem>>("Vocabulary");
            var vocabulary = new Vocabulary();
            if (vocabularyItems != null)
            {
                vocabulary.Import(vocabularyItems);
            }
            return vocabulary;
        }

        public static void SetupDefaultConvention(IServiceConfigurationProvider container)
        {
            container.Add(AssemblyScanner.ForAssamby<DependencyResolverInitialization>().GetInterfaceWithDefaultConventions());
        }

        private static void SetupFeatureModules(IServiceConfigurationProvider container)
        {
            try
            {
                var types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("MovieShop")).SelectMany(s => s.GetTypes()).Where(p => typeof(IDependencyInjectionConfig).IsAssignableFrom(p) && !p.IsInterface);
                foreach (var type in types)
                {
                    var methodInfo = type.GetMethod(nameof(IDependencyInjectionConfig.Setup));
                    var classInstance = Activator.CreateInstance(type, null);
                    methodInfo.Invoke(classInstance, new object[] { container });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}