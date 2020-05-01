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

            var addressModel = new CreateOrUpdateCustomerResponce();
            var cart = await _mediator.Send(CartContentRequest.Create());
            var checkoutModel = new CheckoutModel() { Customer = addressModel, Cart = cart };
            var viewModel = await _viewModelFactory.Create(currentPage, checkoutModel);
            return View("~/Features/CheckOut/CheckOutPage.cshtml", viewModel);
        }

        [Route("checkout/AddEmail")]
        [HttpPost]
        public async Task<ActionResult> AddEmail(int contentId, string command, string firstName, string familyName, string email)
        {
            var currentPage = _contentLoader.Get<CheckOutPage>(new ContentReference(contentId));
            var request = new CreateOrUpdateCustomerRequest() { Email = email, familyName = familyName, FirstName = firstName };

            var customer = await _mediator.Send(request);
            var cart = await _mediator.Send(CartContentRequest.Create());

            var checkoutModel = new CheckoutModel() { Customer = customer, Step = NextStep(command), Cart = cart };
            var viewModel = await _viewModelFactory.Create(currentPage, checkoutModel);
            return View("~/Features/CheckOut/CheckOutPage.cshtml", viewModel);
        }

        private string NextStep(string command)
        {
            if (command == "Edit name")
            {
                return "step1";
            }
            if (command == "To address")
            {
                return "step2";
            }
            if (command == "Edit address")
            {
                return "step2";
            }
            if (command == "To payment")
            {
                return "step3";
            }

            return "step10";
        }
    }
}