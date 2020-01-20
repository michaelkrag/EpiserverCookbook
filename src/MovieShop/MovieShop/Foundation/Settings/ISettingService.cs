using CommonLib.Monads;
using EPiServer.Core;

namespace MovieShop.Foundation.Settings
{
    public interface ISettingService
    {
        Maybe<TSetting> GetSetting<TSetting>(IContent content) where TSetting : ISettingsBlock;

        Maybe<TSetting> GetSetting<TSetting>(ContentReference contentReference) where TSetting : ISettingsBlock;
    }
}