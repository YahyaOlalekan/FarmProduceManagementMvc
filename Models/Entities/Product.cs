using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Models.Entities
{
    public class Product : BaseEntity
    {
        public string ProduceId { get; set; }
        public Produce Produce { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public double QuantityToSell { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public bool IsAvailable { get; set; }
         public Status Status { get; set; } = Status.Pending;
        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
    }
}
