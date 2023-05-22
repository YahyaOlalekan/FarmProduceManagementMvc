namespace FarmProduceManagement.Models.Entities
{
    public class Order : BaseEntity
    {
         public string OrderNumber {get;set;}
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public double TotalQuantity { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
    }
}
