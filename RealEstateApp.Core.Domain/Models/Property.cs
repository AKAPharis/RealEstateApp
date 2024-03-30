namespace RealEstateApp.Core.Domain.Models
{
    public class Property
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
        public List<Upgrades> Upgrades { get; set; }
        public TypeOfSale TypeOfSale { get; set; }
        public TypeProperty TypeProperty { get; set; }


    }

}
