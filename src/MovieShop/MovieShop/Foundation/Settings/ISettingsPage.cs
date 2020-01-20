using EPiServer.Core;

namespace MovieShop.Foundation.Settings
{
    public interface ISettingsPage : IContent
    {
        ContentArea SettingsArea { get; }
    }
}