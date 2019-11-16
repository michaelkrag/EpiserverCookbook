using System;

namespace MovieShop.Business.Services.Blobstore
{
    public interface IBlobRepository
    {
        Uri Save<TObj>(string name, TObj data);

        TObj Load<TObj>(string name);
    }
}