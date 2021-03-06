﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLPLibTest.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLPLib.Vocabularys;
using NLPLib.Search.DocumentStores;
using NLPLib.Tokenizers;
using NLPLib.Search;
using System.Diagnostics;

namespace NLPLibTest.Search
{
    [TestClass]
    public class IrtRetSearchTest
    {
        private List<Document> docs = new List<Document>()
        {
            new Document() { Title = "Pride Bushido 9", Body = "Pride Bushido 9 was a mixed martial arts event held by Pride Fighting Championships. This event held the quarterfinal and semifinal rounds of the first ever Welterweight tournament and Lightweight tournament. It took place at the Ariake Coliseum in Tokyo, Japan on September 25, 2005. Under BUSHIDO rules, matches are 2 rounds only.", Year = 2005 },
            new Document() { Title = "Back to the future",          Body = "Marty McFly, a 17-year-old high school student, is accidentally sent thirty years into the past in a time-traveling DeLorean invented by his close friend, the eccentric scientist Doc Brown.", Year = 1985 },
            new Document() { Title = "Back to the Future Part II",  Body = "After visiting 2015, Marty McFly must repeat his visit to 1955 to prevent disastrous changes to 1985...without interfering with his first trip.", Year = 1989 },
            new Document() { Title = "Back to the Future Part III", Body = "Stranded in 1955, Marty McFly learns about the death of Doc Brown in 1885 and must travel back in time to save him. With no fuel readily available for the DeLorean, the two must figure out a way to escape the Old West before Emmett is murdered.", Year= 1990 }
        };

        [TestMethod]
        public void TestMethod1()
        {
            var tokinizer = new Tokinizer(new HashSet<string>());
            var vocabulary = new Vocabulary();
            IrtRetSearch search = new IrtRetSearch(vocabulary, new DocumentStorageMemory(), tokinizer);
            var index = 0;
            foreach (var doc in docs)
            {
                search.Indexing(index, doc);
                index++;
            }
            var docSearch2 = search.Search<Document>("Back to the Future", 10).OrderByDescending(x => x.Score).ToList();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var tokinizer = new Tokinizer(new HashSet<string>());
            var vocabulary = new Vocabulary();

            var search = new SearchEngine(vocabulary, new DocumentStorageMemory(), tokinizer);
            var index = 0;
            foreach (var doc in docs)
            {
                search.Indexing(index, doc);
                index++;
            }
            var docSearch2 = search.Query().MultiMatch("Back to the Future",
                    new List<MatchField<Document>>()
                    {
                        new MatchField<Document>() { field = x => x.Title, Boost = 1 },
                        new MatchField<Document>() { field = x => x.Body}
                    }).GetSearchHits<Document>();

            var json = search.Export();
        }
    }
}