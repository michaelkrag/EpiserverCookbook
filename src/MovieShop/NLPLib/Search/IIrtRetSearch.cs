﻿using NLPLib.Search.Models;
using System.Collections.Generic;

namespace NLPLib.Search
{
    public interface IIrtRetSearch
    {
        IEnumerable<SearchHit<TObj>> Search<TObj>(string str, int numberOfDucuments) where TObj : class;
    }
}