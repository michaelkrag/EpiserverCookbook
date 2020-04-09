using EPiServer;
using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.Security;
using EPiServer.DataAbstraction;
using Mediachase.Commerce.Catalog;
using EPiServer.Commerce.Security;

namespace MovieShop.Business.ScheduledJob
{
    [ScheduledPlugIn(DisplayName = "Fix", GUID = "D6DEAA26-9766-4140-BE18-2D6D091A3A35")]
    public class FixScheduledJob : ScheduledJobBase
    {
        private readonly IContentLoader _contentLoader;
        private readonly ReferenceConverter _referenceConverter;
        private readonly IContentSecurityRepository _contentSecurityRepository;

        public FixScheduledJob(IContentLoader contentLoader, ReferenceConverter referenceConverter, IContentSecurityRepository contentSecurityRepository)
        {
            _contentLoader = contentLoader;
            _referenceConverter = referenceConverter;
            _contentSecurityRepository = contentSecurityRepository;
        }

        public override string Execute()
        {
            if (_contentLoader.TryGet(_referenceConverter.GetRootLink(), out IContent content))
            {
                var securableContent = (IContentSecurable)content;
                var defaultAccessControlList = (IContentSecurityDescriptor)securableContent.GetContentSecurityDescriptor().CreateWritableClone();
                defaultAccessControlList.AddEntry(new AccessControlEntry(RoleNames.CommerceAdmins, AccessLevel.FullAccess, SecurityEntityType.Role));
                defaultAccessControlList.AddEntry(new AccessControlEntry(EveryoneRole.RoleName, AccessLevel.Read, SecurityEntityType.Role));

                _contentSecurityRepository.Save(content.ContentLink, defaultAccessControlList, SecuritySaveType.Replace);
                return "fix";
            }
            return "nothing to fix";
        }
    }
}