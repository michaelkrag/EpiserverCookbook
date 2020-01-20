using CommonLib.Monads;
using EPiServer;
using EPiServer.Core;
using MovieShop.Foundation.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MovieShop.Foundation.Settings
{
    public class SettingService : ISettingService
    {
        private readonly IContentLoader _contentLoader;

        public SettingService(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public Maybe<TSetting> GetSetting<TSetting>(IContent content) where TSetting : ISettingsBlock
        {
            return GetSetting<TSetting>(content.ContentLink);
        }

        public Maybe<TSetting> GetSetting<TSetting>(ContentReference contentReference) where TSetting : ISettingsBlock
        {
            var setting = contentReference.ToMaybe()
                                          .Map(x => GetSettingsPages(contentReference))
                                          .Map<IEnumerable<TSetting>>(x => x.SelectMany(y => y?.SettingsArea.GetBlockList<TSetting>()))
                                          .Map(x => x.Where(y => y != null))
                                          .Map(x => x.FirstOrDefault());

            return setting;
        }

        private IEnumerable<ISettingsPage> GetSettingsPages(ContentReference contentReference)
        {
            foreach (var ancestor in Enumerable.Repeat(contentReference, 1).Concat(_contentLoader.GetAncestors(contentReference)?.Select(c => c.ContentLink)))
            {
                if (_contentLoader.TryGet<ISettingsPage>(ancestor, out var settingPage))
                {
                    yield return settingPage;
                }
            }
        }
    }
}