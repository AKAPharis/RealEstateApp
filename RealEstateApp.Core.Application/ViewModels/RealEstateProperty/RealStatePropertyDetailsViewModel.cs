using RealEstateApp.Core.Application.ViewModels.TypeOfProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.RealEstateProperty
{
    public class RealStatePropertyDetailsViewModel
    {
        public string Guid { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public string AgentName { get; set; }
        public string AgentLastName { get; set; }
        public string AgentPhoneNumber { get; set; }
        public string AgentEmail { get; set; }
        public string AgentId { get; set; }
        public int TypeOfSaleId { get; set; }
        public TypeOfSaleViewModel TypeOfSale { get; set; }
        public int TypePropertyId { get; set; }
        public TypeOfPropertyViewModel TypeProperty { get; set; }
        public ICollection<UpgradeViewModel> Upgrades { get; set; }
        public ICollection<string> Images { get; set; }
    }
}
