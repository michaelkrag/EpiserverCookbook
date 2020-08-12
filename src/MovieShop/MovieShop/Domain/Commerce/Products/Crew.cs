using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;

namespace MovieShop.Domain.Commerce.Products
{
    public class Crew
    {
        public virtual string Job { get; set; }
        public virtual string Name { get; set; }
        public virtual int Gender { get; set; }
    }

    [PropertyDefinitionTypePlugIn]
    public class CrewProperty : PropertyList<Crew>
    {
    }
}