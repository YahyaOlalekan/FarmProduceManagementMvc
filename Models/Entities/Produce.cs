using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Models.Entities
{
    public class Produce : BaseEntity
    {
        public string ProduceName { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public double QuantityToBuy { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public ICollection<TransactionProduce> TransactionProduces { get; set; } = new HashSet<TransactionProduce>();
    }
}