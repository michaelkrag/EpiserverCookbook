using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using MovieShop.Business.Repository.EvidenceCollector;
using MovieShop.Business.Repository.EvidenceCollector.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MovieShop.Infrastructure.initialize
{
    [InitializableModule]
    public class AssociationsGroupsInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            /*try
            {
                using (var db = new EvidenceContext())
                {
                    db.Evidences.Add(new Evidence() { ContentId = "a", Event = "b", SessionId = "c", Id = Guid.NewGuid(), UserId = "d", Date = DateTime.UtcNow });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("hej");
            }*/

            var associationDefinitionRepository = ServiceLocator.Current.GetInstance<GroupDefinitionRepository<AssociationGroupDefinition>>();
            associationDefinitionRepository.Add(new AssociationGroupDefinition { Name = "CrossSell" });
            associationDefinitionRepository.Add(new AssociationGroupDefinition { Name = "Replacement" });
            associationDefinitionRepository.Add(new AssociationGroupDefinition { Name = "UpSell" });
            associationDefinitionRepository.Add(new AssociationGroupDefinition { Name = "Optional" });
            associationDefinitionRepository.Add(new AssociationGroupDefinition { Name = "Requiresd" });
        }

        public void Uninitialize(InitializationEngine context)
        {
            throw new NotImplementedException();
        }
    }
}