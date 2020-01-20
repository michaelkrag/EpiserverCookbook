using EPiServer.Shell;

namespace MovieShop.Features.Catalog.Container
{
    [UIDescriptorRegistration]
    public class ContainerPageUIDescriptor : UIDescriptor<ContainerPage>
    {
        public ContainerPageUIDescriptor() : base("epi-iconObjectPage")
        {
        }
    }
}