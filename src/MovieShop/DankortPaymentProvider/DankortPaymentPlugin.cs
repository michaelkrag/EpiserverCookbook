using EPiServer.Commerce.Order;
using Mediachase.Commerce.Orders;
using System.Collections.Generic;

namespace DankortPaymentProvider
{
    public class DankortPaymentPlugin : IPaymentPlugin
    {
        public IDictionary<string, string> Settings { get; set; }

        public PaymentProcessingResult ProcessPayment(IOrderGroup orderGroup, IPayment payment)
        {
            var creditLimit = 500;
            payment.TransactionType = TransactionType.Sale.ToString();
            if (payment.Amount <= creditLimit)
            {
                return PaymentProcessingResult.CreateSuccessfulResult($"godkendt betaling for {payment.Amount}, key {Settings["SecretKeyExample"]}");
            }
            else
            {
                return PaymentProcessingResult.CreateUnsuccessfulResult($"beklager du er over limit!!!!");
            }
        }
    }
}