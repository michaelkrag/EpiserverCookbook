using MediatR;
using MovieShop.Domain.MediaR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MovieShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("cart")]
        [HttpPost]
        public async Task<ActionResult> Add(string sku, int quantity)
        {
            var result = await _mediator.Send(CartAddRequest.Create(sku, quantity));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route("cart/sku")]
        [HttpPut]
        public ActionResult Update(string sku, int quantity)
        {
            var obj = new { status = "ok" };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [Route("cart/sku")]
        [HttpDelete]
        public ActionResult Delete(string sku)
        {
            var obj = new { status = "ok" };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [Route("cart")]
        [HttpGet]
        public ActionResult Cart()
        {
            var obj = new { status = "ok" };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}