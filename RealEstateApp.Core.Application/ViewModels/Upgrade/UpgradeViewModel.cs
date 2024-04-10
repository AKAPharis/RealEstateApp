using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;

namespace RealEstateApp.Core.Application.ViewModels.Upgrade
{
    public class UpgradeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RealEstatePropertyViewModel> Properties { get; set; }
    }
}
