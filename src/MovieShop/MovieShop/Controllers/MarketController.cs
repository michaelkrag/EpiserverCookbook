using Mediachase.Commerce;
using Mediachase.Commerce.Markets;
using MovieShop.Business.Currencys;
using MovieShop.Domain.Component;
using System.Net;

using System.Web.Mvc;

namespace MovieShop.Controllers
{
    public class MarketController : Controller
    {
        private readonly ICurrentMarket _currentMarket;
        private readonly ICurrentCurrency _currentCurrency;

        public MarketController(ICurrentMarket currentMarket, ICurrentCurrency currentCurrency)
        {
            _currentMarket = currentMarket;
            _currentCurrency = currentCurrency;
        }

        [Route("SetMarket")]
        [HttpGet]
        public ActionResult SetMarket(string id, string rtnUrl)
        {
            var code = MarketCurrency.Create(id);

            _currentMarket.SetCurrentMarket(code.MarketId);
            _currentCurrency.SetCurrentCurrency(code.CurrentCode);

            var redirectUrl = !string.IsNullOrEmpty(rtnUrl) ? rtnUrl : "/";

            return Redirect(redirectUrl);
        }
    }
}