using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Services.Blobstore.Models
{
    public class FileInfo : IDynamicData
    {
        public Identity Id { get; set; }
        public string Name { get; set; }
        public Uri BlobId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}