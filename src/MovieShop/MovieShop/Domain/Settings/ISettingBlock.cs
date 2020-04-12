using EPiServer.Core;
using EPiServer.Shell;

namespace MovieShop.Domain.Settings
{
    public interface ISettingBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class SettingBlockUIDescriptor : UIDescriptor<ISettingBlock>
    {
    }
}