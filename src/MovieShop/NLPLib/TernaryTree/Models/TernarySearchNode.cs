using System.Collections.Generic;
using System.Linq;

namespace NLPLib.TernaryTree.Models
{
    public class TernarySearchNode
    {
        private static char EmptyChar = '~';

        public class WordEntry
        {
            public string Word { get; set; }
            public int Occurrence { get; set; }
        }

        public bool WordNode { get; set; } = false;
        public char SplitChar { get; set; } = EmptyChar;
        public TernarySearchNode LowNode { get; set; } = null;
        public TernarySearchNode EqNode { get; set; } = null;
        public TernarySearchNode HiNode { get; set; } = null;

        public string Word { get; set; }
        public int Occurrence { get; set; }

        public bool IsEmpty()
        {
            return SplitChar == EmptyChar;
        }

        public TernarySearchNode GetEqNode()
        {
            if (EqNode == null)
            {
                EqNode = new TernarySearchNode();
            }
            return EqNode;
        }

        public TernarySearchNode GetLowNode()
        {
            if (LowNode == null)
            {
                LowNode = new TernarySearchNode();
            }
            return LowNode;
        }

        public TernarySearchNode GetHiNode()
        {
            if (HiNode == null)
            {
                HiNode = new TernarySearchNode();
            }
            return HiNode;
        }

        public void AppendMaybe(string word, int occ)
        {
            Maybe.Add(new WordEntry() { Word = word, Occurrence = occ });
            Maybe = Maybe.OrderByDescending(x => x.Occurrence).Take(8).ToList();
        }

        public List<WordEntry> Maybe { get; private set; } = new List<WordEntry>();

        public override string ToString()
        {
            return $"Current sc: {SplitChar.ToString()} -> L: {LowNode?.SplitChar ?? '#'} , E: {EqNode?.SplitChar ?? '#'} H: {HiNode?.SplitChar ?? '#'}";
        }
    }
}