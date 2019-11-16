using System;

namespace MovieShop.Business.Services.Blobstore
{
    public interface IBlobFilenameRepository
    {
        bool HasFile(string name);

        void Save(string name, Uri blobId);

        bool TryGet(string name, out Uri uri);
    }
}