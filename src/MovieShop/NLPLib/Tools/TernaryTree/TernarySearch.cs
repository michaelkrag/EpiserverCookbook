using NLPLib.Tools.TernaryTree.Models;
using System.Collections.Generic;
using System.Linq;

namespace NLPLib.Tools.TernaryTree
{
    public class TernarySearch<TObj> : ISearch<TObj>
    {
        private TernarySearchNode<TObj> Root = null;

        public void Add(string word, TObj value)
        {
            Root = Add(Root, word.ToLower(), 0, value);
        }

        private TernarySearchNode<TObj> Add(TernarySearchNode<TObj> p, string word, int pos, TObj value)
        {
            if (p == null)
            {
                p = new TernarySearchNode<TObj>(word[pos]);
            }

            if (word[pos] < p.SplitChar)
            {
                p.LowNode = Add(p.LowNode, word, pos, value);
            }
            else if (word[pos] == p.SplitChar)
            {
                if (pos < word.Length - 1)
                {
                    p.EqNode = Add(p.EqNode, word, ++pos, value);
                }
                else
                {
                    p.Value = value;
                }
            }
            else
            {
                p.HiNode = Add(p.HiNode, word, pos, value);
            }

            return p;
        }

        public IEnumerable<TObj> Search(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return Enumerable.Empty<TObj>();
            }
            var prefixRoot = CrawlToPrefixLastNode(Root, word, 0);
            if (prefixRoot == null)
            {
                return Enumerable.Empty<TObj>();
            }
            var result = FindAllSuggestions(prefixRoot, word);

            return result;
        }

        public IEnumerable<TObj> Search(IEnumerable<string> words)
        {
            var rtnList = words.SelectMany(x => Search(x)).Distinct();
            return rtnList;
        }

        private TernarySearchNode<TObj> CrawlToPrefixLastNode(TernarySearchNode<TObj> tNode, string word, int ptr)
        {
            if (tNode == null)
                return null;
            if (word[ptr] < tNode.SplitChar)
                return CrawlToPrefixLastNode(tNode.LowNode, word, ptr);
            else if (word[ptr] > tNode.SplitChar)
                return CrawlToPrefixLastNode(tNode.HiNode, word, ptr);
            else
            {
                if (ptr == word.Length - 1)
                    return tNode;
                else
                    return CrawlToPrefixLastNode(tNode.EqNode, word, ptr + 1);
            }
        }

        private IEnumerable<TObj> FindAllSuggestions(TernarySearchNode<TObj> tNode, string word)
        {
            var nodeQueue = new Queue<TernarySearchNode<TObj>>();

            if (tNode.Value != null)
            {
                yield return tNode.Value;
            }

            if (tNode.EqNode != null)
            {
                nodeQueue.Enqueue(tNode.EqNode);
            }

            while (nodeQueue.Any())
            {
                var node = nodeQueue.Dequeue();

                if (node.Value != null)
                {
                    yield return node.Value;
                }
                if (node.LowNode != null)
                {
                    nodeQueue.Enqueue(node.LowNode);
                }
                if (node.EqNode != null)
                {
                    nodeQueue.Enqueue(node.EqNode);
                }
                if (node.HiNode != null)
                {
                    nodeQueue.Enqueue(node.HiNode);
                }
            }
        }
    }
}