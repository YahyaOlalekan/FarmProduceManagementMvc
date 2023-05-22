using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class CartItem : BaseEntity
    {
        public string ProduceId { get; set; }
        public Produce Produce { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string NameOfCategory { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}