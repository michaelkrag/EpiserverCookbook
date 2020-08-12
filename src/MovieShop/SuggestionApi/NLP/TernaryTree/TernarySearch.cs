using SuggestionApi.NLP.TernaryTree.Models;
using SuggestionApi.NLP.Vocabularys;
using System.Collections.Generic;
using System.Linq;

namespace SuggestionApi.NLP.TernaryTree
{
    public class TernarySearch : ITernarySearch
    {
        private TernarySearchNode Root = new TernarySearchNode();

        public void Insert(string word, int occurrence)
        {
            var currentNode = Root;
            var currentIndex = 0;
            while (currentIndex < word.Length)
            {
                if (currentNode.IsEmpty())
                {
                    currentNode.SplitChar = word[currentIndex];
                    if (currentIndex == word.Length - 1) //end
                    {
                        currentNode.WordNode = true;
                        currentNode.Word = word;
                        currentNode.Occurrence = occurrence;
                        return;
                    }
                    else
                    {
                        currentNode.AppendMaybe(word, occurrence);
                        currentNode = currentNode.GetEqNode();
                        currentIndex++;
                    }
                }
                else if (word[currentIndex] == currentNode.SplitChar)
                {
                    currentNode.AppendMaybe(word, occurrence);
                    currentNode = currentNode.GetEqNode();
                    currentIndex++;
                }
                else if (word[currentIndex] < currentNode.SplitChar)
                {
                    currentNode = currentNode.GetLowNode();
                }
                else
                {
                    currentNode = currentNode.GetHiNode();
                }
            }
        }

        public IEnumerable<string> Compleate(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return Enumerable.Empty<string>();
            }
            var prefixRoot = CrawlToPrefixLastNode(word);
            if (prefixRoot == null)
            {
                return Enumerable.Empty<string>();
            }

            var result = new List<string>();
            if (prefixRoot.WordNode)
            {
                result.Add(prefixRoot.Word);
            }

            result.AddRange(prefixRoot.Maybe.Select(x => x.Word));

            return result;
        }

        private TernarySearchNode CrawlToPrefixLastNode(string word)
        {
            var currentNode = Root;
            if (string.IsNullOrEmpty(word))
            {
                return currentNode;
            }
            var currentPos = 0;
            while (currentNode != null)
            {
                if (word[currentPos] < currentNode.SplitChar)
                {
                    currentNode = currentNode.LowNode;
                }
                else if (word[currentPos] > currentNode.SplitChar)
                {
                    currentNode = currentNode.HiNode;
                }
                else
                {
                    if (currentPos == word.Length - 1)
                    {
                        return currentNode;
                    }
                    currentNode = currentNode.EqNode;
                    currentPos++;
                }
            }
            return currentNode;
        }
    }
}