using EPiServer.Framework.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MovieShop.Business.Services.Blobstore
{
    public class BlobRepository : IBlobRepository
    {
        private readonly IBlobFactory _blobFactory;
        private readonly IBlobFilenameRepository _blobFilenameRepository;

        public BlobRepository(IBlobFactory blobFactory, IBlobFilenameRepository blobFilenameRepository)
        {
            _blobFactory = blobFactory;
            _blobFilenameRepository = blobFilenameRepository;
        }

        public Uri Save<TObj>(string name, TObj data)
        {
            var container = Blob.GetContainerIdentifier(Guid.NewGuid());
            var blob = _blobFactory.CreateBlob(container, ".json");

            var json = JsonConvert.SerializeObject(data);
            using (var s = blob.OpenWrite())
            using (var w = new StreamWriter(s))
            {
                w.Write(json);
                w.Flush();
            }

            if (_blobFilenameRepository.TryGet(name, out var uri))
            {
                Delete(uri);
            }
            _blobFilenameRepository.Save(name, blob.ID);
            return blob.ID;
        }

        public TObj Load<TObj>(string name)
        {
            if (_blobFilenameRepository.TryGet(name, out var uri))
            {
                var blob = _blobFactory.GetBlob(uri);
                using (var s = blob.OpenRead())
                {
                    var json = new StreamReader(s).ReadToEnd();
                    return JsonConvert.DeserializeObject<TObj>(json);
                }
            }
            return default(TObj);
        }

        public void Delete(Uri uri)
        {
            try
            {
                var blob = _blobFactory.GetBlob(uri);
                _blobFactory.Delete(blob.ID);
                _blobFactory.Delete(uri);
            }
            catch (Exception ex)
            {
            }
        }
    }
}