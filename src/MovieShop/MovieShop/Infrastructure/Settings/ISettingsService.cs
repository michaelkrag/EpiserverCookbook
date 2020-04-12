using MovieShop.Domain.Settings;

namespace MovieShop.Infrastructure.Settings
{
    public interface ISettingsService
    {
        TSettings GetSetting<TSettings>() where TSettings : ISettingBlock;
    }
}