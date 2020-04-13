using NLPLib.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NLPLib.Search
{
    public interface IQuery
    {
        IFilter MultiMatch<TObj>(string query, IEnumerable<MatchField<TObj>> fields);

        IFilter Filer<TObj>(Expression<Func<TObj, bool>> field);
    }

    public interface IFilter
    {
        IFilter Filer<TObj>(Expression<Func<TObj, bool>> field);

        IEnumerable<SearchHit<TObj>> GetSearchHits<TObj>(int take = 10, int skib = 0) where TObj : class;
    }
}