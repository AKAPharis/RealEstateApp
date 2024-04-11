using RealEstateApp.Core.Application.ViewModels.Common;
using RealEstateApp.Core.Application.ViewModels.TypeOfProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using RealEstateApp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.RealEstateProperty
{
    public class RealEstatePropertyViewModel : BaseViewModel
    {

        public string Guid { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public List<string> Images { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public int TypeOfSaleId { get; set; }
        public TypeOfSaleViewModel TypeOfSale { get; set; }
        public int TypePropertyId { get; set; }
        public TypeOfPropertyViewModel TypeProperty { get; set; }
        public ICollection<UpgradeViewModel> Upgrades { get; set; }
        public ICollection<string> Images { get; set; }


    }
}
