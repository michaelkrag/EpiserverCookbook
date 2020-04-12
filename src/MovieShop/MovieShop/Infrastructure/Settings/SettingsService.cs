using EPiServer;
using EPiServer.Core;
using MovieShop.Business.Extensions;
using MovieShop.Domain.Settings;
using System.Collections.Generic;
using System.Linq;

namespace MovieShop.Infrastructure.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly IContentLoader _contentLoader;

        public SettingsService(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public TSettings GetSetting<TSettings>() where TSettings : ISettingBlock
        {
            var content = _contentLoader.Get<IContent>(ContentReference.StartPage);
            return GetSetting<TSettings>(content);
        }

        public TSettings GetSetting<TSettings>(ContentReference page) where TSettings : ISettingBlock
        {
            var content = _contentLoader.Get<IContent>(page);
            return GetSetting<TSettings>(content);
        }

        public TSettings GetSetting<TSettings>(IContent page) where TSettings : ISettingBlock
        {
            var setting = GetSettingsPages(page).SelectMany(x => x.Settings.GetBlockList<TSettings>()).FirstOrDefault();
            return setting;
        }

        private IEnumerable<ISettingsPage> GetSettingsPages(IContent content)
        {
            var rtnList = new List<ISettingsPage>();
            var ancenters = Enumerable.Repeat(content, 1).Concat(_contentLoader.GetAncestors(content.ContentLink));

            foreach (var ancenter in ancenters)
            {
                if (ancenter is ISettingsPage settingsPage)
                {
                    rtnList.Add(settingsPage);
                }
                var settingsPages = _contentLoader.GetChildren<ISettingsPage>(ancenter.ContentLink);
                rtnList.AddRange(settingsPages);
            }
            return rtnList;
        }
    }
}