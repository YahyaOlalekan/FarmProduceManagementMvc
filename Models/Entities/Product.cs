namespace FarmProduceManagement.Models.Entities
{
    public class Product : BaseEntity
    {
        public string ProduceName { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public double QuantityToSell { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
    }
}
