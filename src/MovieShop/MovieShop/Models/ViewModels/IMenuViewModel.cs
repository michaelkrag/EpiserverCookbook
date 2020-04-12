using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Models.ViewModels
{
    public class MenuItem
    {
        public string Title { get; set; }
        public ContentReference Link { get; set; }
        public string MouseOverText { get; set; }
    }

    public interface IMenuViewModel
    {
        string RecommendTitle { get; }
        IEnumerable<MenuItem> Recommend { get; }
        string CategoriesTitle { get; }
        IEnumerable<MenuItem> Categories { get; }
        string CartUrl { get; }

        bool ShowRecommendTitle();

        bool ShowCategoriesTitle();
    }
}