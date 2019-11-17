using System.Collections.Generic;

namespace NLPLib.Tools.TernaryTree
{
    public interface ISearch<TObj>
    {
        IEnumerable<TObj> Search(string word);

        IEnumerable<TObj> Search(IEnumerable<string> word);
    }
}