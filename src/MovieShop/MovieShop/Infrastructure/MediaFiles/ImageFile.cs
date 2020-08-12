using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Infrastructure.MediaFiles
{
    [ContentType(GUID = "6C510D19-0A1A-420A-8F56-F7C3560F9CDC")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageFile : CommerceImage
    {
        public virtual string Title { get; set; }
        public virtual string Copyright { get; set; }

        [UIHint(UIHint.Textarea)]
        public virtual string Description { get; set; }
    }
}