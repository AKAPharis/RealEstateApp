using RealEstateApp.Core.Domain.Common;

namespace RealEstateApp.Core.Domain.Models
{
    public class TypeProperty : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
