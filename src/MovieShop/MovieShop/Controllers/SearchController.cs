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
        private readonly IAutocompleateService _autocompleateService;

        public SearchController(IAutocompleateService autocompleateService)
        {
            _autocompleateService = autocompleateService;
        }

        // GET: Search
        [Route("autocomplete")]
        [HttpGet]
        public ActionResult Search(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var list = _autocompleateService.GetSuggestions(q);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json(Enumerable.Empty<string>(), JsonRequestBehavior.AllowGet);
        }
    }
}