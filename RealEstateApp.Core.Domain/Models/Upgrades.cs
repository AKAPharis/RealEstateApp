using RealEstateApp.Core.Domain.Common;

namespace RealEstateApp.Core.Domain.Models
{
    public class Upgrades : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
