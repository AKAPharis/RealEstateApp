using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;

namespace RealEstateApp.Core.Application.ViewModels.TypeOfProperty
{
    public class TypeOfPropertyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RealEstatePropertyViewModel> Properties { get; set; }
    }
}
