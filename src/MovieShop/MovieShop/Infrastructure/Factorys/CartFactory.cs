using EPiServer.Commerce.Order;
using Mediachase.Commerce.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Infrastructure.Factorys
{
    public class CartFactory : ICartFactory
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CustomerContext _customerContext;

        public CartFactory(IOrderRepository orderRepository, CustomerContext customerContext)
        {
            _orderRepository = orderRepository;
            _customerContext = customerContext;
        }

        public ICart LoadOrCreateCart(string name = "Default")
        {
            var cart = _orderRepository.LoadOrCreateCart<ICart>(_customerContext.CurrentContactId, name);
            return cart;
        }

        public void Save(ICart cart)
        {
            var orderReference = _orderRepository.Save(cart);
        }
    }
}

/*

     var cart = OrderRepository.LoadOrCreate<Cart>(PrincipalInfo.CurrentPrincipal.GetContactId(), Cart.DefaultName);
var price = PriceService.GetDefaultPrice(marketId,
        DateTime.UtcNow,
        new CatalogKey(new Guid(variation.ApplicationId), variation.Code),
        SiteContext.Current.Currency);

var lineItem = CreateLineItem(variation, quantity, price.UnitPrice.Amount);
cart.Forms.First().Shipments.First().LineItems.Add(lineItem);
PromotionEngine.Run(cart);
OrderRepository.Save(cart);

     */