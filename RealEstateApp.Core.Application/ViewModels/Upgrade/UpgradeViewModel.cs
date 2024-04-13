using RealEstateApp.Core.Application.ViewModels.Common;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Upgrade
{
    public class UpgradeViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RealEstatePropertyViewModel> Properties { get; set; }

    }
}
