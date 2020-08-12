using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.Web;

namespace MovieShop.Business.Services.ImageStore
{
    public static class ContentRepositoryExtension
    {
        public static ContentReference CreateFolder(this IContentRepository contentRepository, string folderName, ContentReference parentContent)
        {
            var contentFile = contentRepository.GetDefault<ContentFolder>(parentContent);
            contentFile.Name = folderName;
            return contentRepository.Save(contentFile, SaveAction.Publish, AccessLevel.NoAccess);
        }
    }
}