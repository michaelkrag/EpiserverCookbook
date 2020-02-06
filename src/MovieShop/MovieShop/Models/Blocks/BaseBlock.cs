using EPiServer.Core;
using EPiServer.DataAbstraction;
using MovieShop.Infrastructure.Helpers;

namespace MovieShop.Models.Blocks
{
    public abstract class BaseBlock : BlockData
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            SetDefaultHelper.MapDefaultValues(this);
        }
    }
}