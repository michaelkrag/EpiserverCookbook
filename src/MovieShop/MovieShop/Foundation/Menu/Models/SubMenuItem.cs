using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Foundation.Menu.Models
{
    public class SubMenuItem
    {
        public string Name { get; set; }
        public ContentReference Reference { get; set; }
    }
}