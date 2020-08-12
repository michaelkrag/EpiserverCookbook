using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Framework.Blobs;
using EPiServer.Security;
using EPiServer.Web;
using MovieShop.Infrastructure.MediaFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieShop.Business.Services.ImageStore
{
    public class ImageRepository : IImageRepository
    {
        private readonly IContentRepository _contentRepository;
        private readonly IBlobFactory _blobFactory;

        public ImageRepository(IContentRepository contentRepository, IBlobFactory blobFactory)
        {
            _contentRepository = contentRepository;
            _blobFactory = blobFactory;
        }

        public ContentReference Insert(string filename, string imageExtension, byte[] data)
        {
            try
            {
                var segments = filename.Split('/');
                var folder = GetOrCreateFolder(segments.Take(segments.Length - 1));

                var file = GetOrCreateFile(segments.Last(), folder);

                file.Name = segments.Last();

                var blob = _blobFactory.CreateBlob(file.BinaryDataContainer, imageExtension);
                using (var s = blob.OpenWrite())
                using (var w = new StreamWriter(s))
                {
                    w.BaseStream.Write(data, 0, data.Length);
                    w.Flush();
                }

                //Assign to file and publish changes
                file.BinaryData = blob;
                var file1ID = _contentRepository.Save(file, SaveAction.Publish, AccessLevel.NoAccess);
                return file1ID;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ImageFile GetOrCreateFile(string fileName, ContentReference parentReference)
        {
            var fileContent = _contentRepository.GetChildren<IContent>(parentReference)?.Where(x => x.Name == fileName).FirstOrDefault();
            if (fileContent != null)
            {
                return _contentRepository.Get<ImageFile>(fileContent.ContentLink).CreateWritableClone() as ImageFile;
            }

            var file = _contentRepository.GetDefault<ImageFile>(parentReference);
            return file;
        }

        private ContentReference GetOrCreateFolder(IEnumerable<string> folderNames)
        {
            try
            {
                var parent = SiteDefinition.Current.GlobalAssetsRoot;
                foreach (var segment in folderNames)
                {
                    var folder = _contentRepository.GetChildren<IContent>(parent)?.Where(x => x.Name == segment).FirstOrDefault()?.ContentLink ??
                        _contentRepository.CreateFolder(segment, parent);
                    parent = folder;
                }
                return parent;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}