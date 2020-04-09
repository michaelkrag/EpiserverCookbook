using EPiServer.Commerce.Order;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Foundation.Extensions
{
    public static class CartExtensions
    {
        private static Injected<IOrderGroupFactory> OrderGroupFactory;
        private static IOrderGroupFactory _orderGroupFactory => OrderGroupFactory.Service;

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
            return cart;
        }

        private static ICart UpdateQuantity(this ICart cart, ILineItem lineItem, int quantity)
        {
            var shipment = cart.GetFirstShipment();
            cart.UpdateLineItemQuantity(shipment, lineItem, lineItem.Quantity + quantity);
            return cart;
        }
    }
}