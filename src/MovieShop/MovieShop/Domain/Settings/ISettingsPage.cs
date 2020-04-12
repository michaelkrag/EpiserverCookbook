using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Domain.Settings
{
    public interface ISettingsPage : IContent
    {
        ContentArea Settings { get; }
    }
}