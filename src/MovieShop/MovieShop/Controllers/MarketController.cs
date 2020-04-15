using Mediachase.Commerce;
using Mediachase.Commerce.Markets;
using System.Net;

using System.Web.Mvc;

namespace MovieShop.Controllers
{
    public class MarketController
    {
        private readonly IMarketService _marketService;
        private readonly ICurrentMarket _currentMarket;

        public MarketController(IMarketService marketService, ICurrentMarket currentMarket)
        {
            _marketService = marketService;
            _currentMarket = currentMarket;
        }

        [Route("SetMarket")]
        [HttpGet]
        public ActionResult SetMarket(string marketId)
        {
            var market = _marketService.GetMarket(marketId);
            if (market != null)
            {
                _currentMarket.SetCurrentMarket(market.MarketId);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}