using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class Produce : BaseEntity
    {
        public string ProduceName { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public double QuantityToBuy { get; set; }
        public decimal CostPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public ICollection<TransactionProduce> TransactionProduces { get; set; } = new HashSet<TransactionProduce>();
    }
}