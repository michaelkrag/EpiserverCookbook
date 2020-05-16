using EPiServer;
using EPiServer.Core;
using Mediachase.Commerce.Orders.Managers;
using MediatR;
using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Domain.MediaR;
using MovieShop.Features.CheckOut.Models;
using MovieShop.Models.ViewModels;
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
            var checkoutModel = await CreateModel(new CheckOutInputModel(), string.Empty);
            var viewModel = await _viewModelFactory.Create(currentPage, checkoutModel);
            return View("~/Features/CheckOut/CheckOutPage.cshtml", viewModel);
        }

        [Route("checkout/AddEmail")]
        [HttpPost]
        public async Task<ActionResult> AddEmail(int contentId, string command, CheckOutInputModel checkOutInputModel)
        {
            if (command == "To address")
            {
                var request = new CreateOrUpdateCustomerRequest() { Email = checkOutInputModel.email, familyName = checkOutInputModel.familyName, FirstName = checkOutInputModel.firstName };
                var customer = await _mediator.Send(request);
            }
            else if (command == "To payment")
            {
                var request = new SetAddressRequest()
                {
                    AddressLine1 = checkOutInputModel.address1,
                    AddressLine2 = checkOutInputModel.address2,
                    City = checkOutInputModel.city,
                    CountryCode = checkOutInputModel.contry,
                    Email = checkOutInputModel.email,
                    PostCode = checkOutInputModel.zip,
                    State = checkOutInputModel.state,
                    FirstName = checkOutInputModel.firstName,
                    LastName = checkOutInputModel.familyName
                };

                var responce = await _mediator.Send(request);
            }

            var currentPage = _contentLoader.Get<CheckOutPage>(new ContentReference(contentId));

            var checkoutModel = await CreateModel(checkOutInputModel, command);

            var viewModel = await _viewModelFactory.Create(currentPage, checkoutModel);
            return View("~/Features/CheckOut/CheckOutPage.cshtml", viewModel);
        }

        public async Task<CheckoutModel> CreateModel(CheckOutInputModel checkOutInputModel, string command)
        {
            var cart = await _mediator.Send(CartContentRequest.Create());
            var jurisdictions = JurisdictionManager.GetJurisdictions(JurisdictionManager.JurisdictionType.Tax);
            var jurisdictionContrys = jurisdictions.Jurisdiction;
            var contrys = jurisdictionContrys.Select(x => new SelectEntry() { DisplayName = x.DisplayName, Key = x.CountryCode, Selected = false }).ToList();

            var checkoutModel = new CheckoutModel() { Customer = checkOutInputModel, Cart = cart, Step = NextStep(command), JurisdictionContrys = contrys };

            return checkoutModel;
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

            return "step1";
        }
    }
}