using EPiServer;
using EPiServer.Core;
using EPiServer.Events.Clients;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MovieShop.Infrastructure.initialize
{
    public class EventsInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //High level
            var contentEvents = context.Locate.ContentEvents();
            contentEvents.PublishedContent += ContentEventPublishedContent;

            //Lowe lvl
            var catalogEvents = ServiceLocator.Current.GetInstance<ICatalogEvents>();

            var eventListener = Event.Get(CatalogEventBroadcaster.CommerceProductUpdated);
        }

        public void Uninitialize(InitializationEngine context)
        {
            var contentEvents = context.Locate.ContentEvents();
            contentEvents.PublishedContent -= ContentEventPublishedContent;
        }

        private void ContentEventPublishedContent(object sender, ContentEventArgs e)
        {
            Debug.WriteLine($"Publish content event fired!  {e.Content.Name}");
        }
    }
}