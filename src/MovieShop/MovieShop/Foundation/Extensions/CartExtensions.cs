using EPiServer.Commerce.Marketing;
using EPiServer.Commerce.Order;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Customers;
using MovieShop.Business.Services.Prices;
using System.Collections.Generic;
using System.Linq;

namespace MovieShop.Foundation.Extensions
{
    public static class CartExtensions
    {
        private static Injected<IOrderGroupFactory> OrderGroupFactory;
        private static IOrderGroupFactory _orderGroupFactory => OrderGroupFactory.Service;
        private static Injected<ICustomerPriceService> CustomerPriceService;
        private static ICustomerPriceService _customerPriceService => CustomerPriceService.Service;

        public static ICart InsertOrUpdate(this ICart cart, string code, int quantity, string displayName)
        {
            var lineItem = cart.GetAllLineItems().FirstOrDefault(x => x.Code == code && !x.IsGift);
            if (lineItem == null)
            {
                cart.Insert(code, quantity, displayName);
            }
            else
            {
                cart.UpdateQuantity(lineItem, quantity);
            }
            return cart;
        }

        private static ICart Insert(this ICart cart, string code, int quantity, string displayName)
        {
            var lineItem = cart.CreateLineItem(code, _orderGroupFactory);
            lineItem.DisplayName = displayName;
            lineItem.Quantity = quantity;
            cart.AddLineItem(lineItem, _orderGroupFactory);

            var price = _customerPriceService.GetPrice(code);
            if (price != null)
            {
                lineItem.PlacedPrice = price.UnitPrice.Amount;
            }

            return cart;
        }

        private static ICart UpdateQuantity(this ICart cart, ILineItem lineItem, int quantity)
        {
            var shipment = cart.GetFirstShipment();
            cart.UpdateLineItemQuantity(shipment, lineItem, lineItem.Quantity + quantity);
            return cart;
        }

        public static Dictionary<ILineItem, IList<ValidationIssue>> ValidateCart(this ICart cart)
        {
            var lineItemValidator = ServiceLocator.Current.GetInstance<ILineItemValidator>();
            var placedPriceProcessor = ServiceLocator.Current.GetInstance<IPlacedPriceProcessor>();
            var inventoryProcessor = ServiceLocator.Current.GetInstance<IInventoryProcessor>();
            var promotionEngine = ServiceLocator.Current.GetInstance<IPromotionEngine>();

            /*    if (cart.Name.Equals(DefaultWishListName))
                {
                    return new Dictionary<ILineItem, List<ValidationIssue>>();
                }*/

            var validationIssues = new Dictionary<ILineItem, IList<ValidationIssue>>();
            cart.ValidateOrRemoveLineItems((item, issue) => validationIssues.AddValidationIssues(item, issue), lineItemValidator);

            cart.UpdatePlacedPriceOrRemoveLineItems(CustomerContext.Current.GetContactById(cart.CustomerId), (item, issue) => validationIssues.AddValidationIssues(item, issue), placedPriceProcessor);

            cart.UpdateInventoryOrRemoveLineItems((item, issue) => validationIssues.AddValidationIssues(item, issue), inventoryProcessor);

            cart.ApplyDiscounts(promotionEngine, new PromotionEngineSettings());

            // Try to validate gift items inventory and don't catch validation issues.
            cart.UpdateInventoryOrRemoveLineItems((item, issue) => { }, inventoryProcessor);

            return validationIssues;
        }
    }

    public static class CartHelperExtensions
    {
        public static void AddValidationIssues(this IDictionary<ILineItem, IList<ValidationIssue>> issues, ILineItem lineItem, ValidationIssue issue)
        {
            if (!issues.ContainsKey(lineItem))
            {
                issues.Add(lineItem, new List<ValidationIssue>());
            }

            if (!issues[lineItem].Contains(issue))
            {
                issues[lineItem].Add(issue);
            }
        }

        public static bool HasItemBeenRemoved(this IDictionary<ILineItem, IList<ValidationIssue>> issuesPerLineItem, ILineItem lineItem)
        {
            IList<ValidationIssue> issues;
            if (issuesPerLineItem.TryGetValue(lineItem, out issues))
            {
                return issues.Any(x => x == ValidationIssue.RemovedDueToInactiveWarehouse ||
                        x == ValidationIssue.RemovedDueToCodeMissing ||
                        x == ValidationIssue.RemovedDueToInsufficientQuantityInInventory ||
                        x == ValidationIssue.RemovedDueToInvalidPrice ||
                        x == ValidationIssue.RemovedDueToMissingInventoryInformation ||
                        x == ValidationIssue.RemovedDueToNotAvailableInMarket ||
                        x == ValidationIssue.RemovedDueToUnavailableCatalog ||
                        x == ValidationIssue.RemovedDueToUnavailableItem);
            }
            return false;
        }
    }
}