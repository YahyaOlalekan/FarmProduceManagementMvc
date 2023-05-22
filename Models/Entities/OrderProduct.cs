using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class OrderProduct : BaseEntity
    {
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

    }
}