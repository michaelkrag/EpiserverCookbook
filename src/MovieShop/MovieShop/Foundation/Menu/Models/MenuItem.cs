using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Foundation.Menu.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public IEnumerable<SubMenuItem> SubMenu { get; set; } = Enumerable.Empty<SubMenuItem>();
    }
}