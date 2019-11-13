using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieShop.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        [Route("autocomplete")]
        [HttpGet]
        public ActionResult Search(string q)
        {
            var obj = new List<string>()
            {
                "mester",
                "bester",
                "vester"
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}