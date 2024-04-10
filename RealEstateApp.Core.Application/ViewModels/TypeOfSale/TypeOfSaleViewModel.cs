using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.TypeOfSale
{
    public class TypeOfSaleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RealEstatePropertyViewModel> Properties { get; set; }
    }
}
