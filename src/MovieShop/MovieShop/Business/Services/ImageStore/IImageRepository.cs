using EPiServer.Core;

namespace MovieShop.Business.Services.ImageStore
{
    public interface IImageRepository
    {
        ContentReference Insert(string filename, string imageExtension, byte[] data);
    }
}