using CommonLib.Monads;
using MovieShop.Business.Services.Search;
using MovieShop.Business.Services.Search.Models;
using NLPLib.TernaryTree;
using NLPLib.Tools.Spelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieShop.Controllers
{
    public class SearchController : Controller
    {
        private ITernarySearch _ternarySearch;

        public SearchController(ITernarySearch ternarySearch)
        {
            _ternarySearch = ternarySearch;
        }

        // GET: Search
        [Route("autocomplete")]
        [HttpGet]
        public ActionResult Search(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var tokens = q.Split(' ');
                ///var result = _ternaryTreeService.Search(q);
                var alphabet = "abcdefghijklmnopqrstuvwxyz";
                //    var sc = new SpellCorrector<TernaryTreeModel>(_ternaryTreeService, alphabet.ToArray());
                var result = _ternarySearch.Compleate(tokens.Last());//sc.Candidates(tokens.Last());
                var list = result.Select(x => x).Take(10);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json(Enumerable.Empty<string>(), JsonRequestBehavior.AllowGet);
        }
    }
}