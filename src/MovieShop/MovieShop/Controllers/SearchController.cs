using MovieShop.Business.Services.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieShop.Controllers
{
    public class SearchController : Controller
    {
        private TernaryTreeService _ternaryTreeService;

        public SearchController(TernaryTreeService ternaryTreeService)
        {
            _ternaryTreeService = ternaryTreeService;
        }

        // GET: Search
        [Route("autocomplete")]
        [HttpGet]
        public ActionResult Search(string q)
        {
            var result = _ternaryTreeService.Search(q);

            var list = result.Select(x => x.Word);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}