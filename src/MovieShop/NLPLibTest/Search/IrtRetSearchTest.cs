using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            new Document() { Title = "Back to the future", Body = "Marty McFly, a 17-year-old high school student, is accidentally sent thirty years into the past in a time-traveling DeLorean invented by his close friend, the eccentric scientist Doc Brown." },
            new Document() { Title = "Back to the Future Part II", Body = "After visiting 2015, Marty McFly must repeat his visit to 1955 to prevent disastrous changes to 1985...without interfering with his first trip." },
            new Document() { Title = "Back to the Future Part III", Body = "Stranded in 1955, Marty McFly learns about the death of Doc Brown in 1885 and must travel back in time to save him. With no fuel readily available for the DeLorean, the two must figure out a way to escape the Old West before Emmett is murdered." },
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
    }
}