using MovieShop.Business.Services.Blobstore;
using NLPLib.Tools.Wordbook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Foundation.Vocabularys
{
    public class VocabularyRepository : IVocabularyRepository
    {
        private const string VocabularyName = "Vocabulary";

        private readonly IBlobRepository _blobRepository;

        public VocabularyRepository(IBlobRepository blobRepository)
        {
            _blobRepository = blobRepository;
        }

        public Vocabulary Get()
        {
            var vocabulary = _blobRepository.Load<Vocabulary>(VocabularyName);
            return vocabulary;
        }

        public void Set(Vocabulary vocabulary)
        {
            _blobRepository.Save(VocabularyName, vocabulary);
        }
    }
}