using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Tools.TernaryTree.Models
{
    public class TernarySearchNode<TObj>
    {
        public char SplitChar { get; private set; } = '#';
        public TObj Value { get; set; }
        public TernarySearchNode<TObj> LowNode { get; set; }
        public TernarySearchNode<TObj> EqNode { get; set; }
        public TernarySearchNode<TObj> HiNode { get; set; }

        public TernarySearchNode(char splitChar)
        {
            SplitChar = splitChar;
            Value = default(TObj);
        }

        public override string ToString()
        {
            return $"{SplitChar} -> ( {LowNode?.SplitChar ?? '#'} ; {EqNode?.SplitChar ?? '#'} ; {HiNode?.SplitChar ?? '#'}";
        }
    }
}