using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.Commerce.Products
{
    public class Cast
    {
        public virtual string CharacterName { get; set; }
        public virtual string Name { get; set; }
        public virtual int Gender { get; set; }
    }

    [PropertyDefinitionTypePlugIn]
    public class CastProperty : PropertyList<Cast>
    {
    }
}