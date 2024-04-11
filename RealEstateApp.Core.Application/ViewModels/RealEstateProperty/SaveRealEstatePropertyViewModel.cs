using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.ViewModels.RealEstateProperty
{
    public class SaveRealEstatePropertyViewModel
    {
        public int? Id { get; set; }
        public string? Guid { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public int TypeOfSaleId { get; set; }
        public int TypePropertyId { get; set; }
        public List<int> Upgrades { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<string>? ImagesPath { get; set; }

    }
}
