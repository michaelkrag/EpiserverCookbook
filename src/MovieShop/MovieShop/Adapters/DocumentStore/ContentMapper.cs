using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Adapters.DocumentStore
{
    public class ContentMapper : IContentMapper
    {
        public TInterface Map<TInterface>(object content) where TInterface : class
        {
            var obj = ObjectGenerator.Generate<TInterface>();
            return obj;
        }
    }
}