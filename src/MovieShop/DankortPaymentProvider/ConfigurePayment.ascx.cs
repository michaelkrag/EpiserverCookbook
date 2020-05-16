using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DankortPaymentProvider
{
    public partial class ConfigurePayment : OrderBaseUserControl, IGatewayControl
    {
        private PaymentMethodDto paymentMethodDto = null;
        private const string secretKeyPayment = "SecretKeyExample";
        public string ValidationGroup { get; set; } = string.Empty;

        public void LoadObject(object dto)
        {
            paymentMethodDto = dto as PaymentMethodDto;
            if (paymentMethodDto != null && paymentMethodDto.PaymentMethodParameter != null)
            {
                var param = GetParameterByName(secretKeyPayment);
                if (param != null)
                {
                    txtSecretKey.Text = param.Value;
                }
            }
        }

        public void SaveChanges(object dto)
        {
            if (Visible)
            {
                paymentMethodDto = dto as PaymentMethodDto;
                if (paymentMethodDto != null && paymentMethodDto.PaymentMethodParameter != null)
                {
                    var paymentMethodId = Guid.Empty;
                    if (paymentMethodDto.PaymentMethod.Count > 0)
                    {
                        paymentMethodId = paymentMethodDto.PaymentMethod[0].PaymentMethodId;
                    }
                    var param = GetParameterByName(secretKeyPayment);
                    if (param != null)
                    {
                        param.Value = txtSecretKey.Text;
                        PaymentManager.SavePayment(paymentMethodDto);
                    }
                    else
                    {
                        var newRow = paymentMethodDto.PaymentMethodParameter.NewPaymentMethodParameterRow();
                        newRow.PaymentMethodId = paymentMethodId;
                        newRow.Parameter = secretKeyPayment;
                        newRow.Value = txtSecretKey.Text;
                        paymentMethodDto.PaymentMethodParameter.Rows.Add(newRow);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private PaymentMethodDto.PaymentMethodParameterRow GetParameterByName(string name)
        {
            var rows = paymentMethodDto.PaymentMethodParameter.Select($"Parameter='{name}'");
            if (rows != null && rows.Length > 0)
            {
                return rows[0] as PaymentMethodDto.PaymentMethodParameterRow;
            }
            return null;
        }
    }
}