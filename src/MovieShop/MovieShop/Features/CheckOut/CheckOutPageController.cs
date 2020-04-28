using EPiServer;
using EPiServer.Core;
using Mediachase.Commerce.Orders.Managers;
using MediatR;
using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Domain.MediaR;
using MovieShop.Features.CheckOut.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieShop.Features.CheckOut
{
    public class CheckOutPageController : BasePageController<CheckOutPage>
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IMediator _mediator;
        private readonly IContentLoader _contentLoader;

        public CheckOutPageController(IViewModelFactory viewModelFactory, IMediator mediator, IContentLoader contentLoader)
        {
            _viewModelFactory = viewModelFactory;
            _mediator = mediator;
            _contentLoader = contentLoader;
        }

        public async Task<ActionResult> Index(CheckOutPage currentPage)
        {
            var jurisdictions = JurisdictionManager.GetJurisdictions(JurisdictionManager.JurisdictionType.Tax);

            var australiaJurisdictions = jurisdictions.Jurisdiction.ToList();
            var jurisdictionGroups = JurisdictionManager.GetJurisdictionGroups(JurisdictionManager.JurisdictionType.Tax);

            var addressModel = new GetOrCreateCustomerResponce();
            var checkoutModel = new CheckoutModel() { Customer = addressModel };
            var viewModel = await _viewModelFactory.Create(currentPage, checkoutModel);
            return View("~/Features/CheckOut/CheckOutPage.cshtml", viewModel);
        }

        [Route("checkout/AddEmail")]
        [HttpPost]
        public async Task<ActionResult> AddEmail(int contentId, string command, string firstName, string familyName, string email)
        {
            var currentPage = _contentLoader.Get<CheckOutPage>(new ContentReference(contentId));
            var request = new GetOrCreateCustomerRequest() { Email = email, familyName = familyName, FirstName = firstName };

            var responce = await _mediator.Send(request);

            var checkoutModel = new CheckoutModel() { Customer = responce, Step = NextStep(command) };
            var viewModel = await _viewModelFactory.Create(currentPage, checkoutModel);
            return View("~/Features/CheckOut/CheckOutPage.cshtml", viewModel);
        }

        private string NextStep(string command)
        {
            if (command == "nameEdit")
            {
                return "step1";
            }
            if (command == "nameSet")
            {
                return "step2";
            }
            return "step2";
        }
    }
}