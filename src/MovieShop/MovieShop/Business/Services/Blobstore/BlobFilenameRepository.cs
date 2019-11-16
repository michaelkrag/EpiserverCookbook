using MovieShop.Business.Repository;
using MovieShop.Business.Services.Blobstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Services.Blobstore
{
    public class BlobFilenameRepository : DynamicDataStoreRepository<FileInfo>, IBlobFilenameRepository
    {
        public bool HasFile(string name)
        {
            return GetFileInfo(name) == null ? false : true;
        }

        public void Save(string name, Uri blobId)
        {
            var file = GetFileInfo(name);

            if (file != null)
            {
                file.BlobId = blobId;
                file.UpdateDate = DateTime.UtcNow;
            }
            else
            {
                file = new FileInfo() { BlobId = blobId, Name = name, CreateDate = DateTime.UtcNow };
            }

            Insert(file);
            return;
        }

        public bool TryGet(string name, out Uri uri)
        {
            var file = GetFileInfo(name);
            if (file != null)
            {
                uri = file.BlobId;
                return true;
            }
            uri = null;
            return false;
        }

        private FileInfo GetFileInfo(string name)
        {
            return this.Items().Where(x => x.Name == name).FirstOrDefault();
        }
    }
}