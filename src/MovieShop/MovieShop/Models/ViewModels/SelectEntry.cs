using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Models.ViewModels
{
    public class SelectEntry
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public bool Selected { get; set; } = false;
    }
}