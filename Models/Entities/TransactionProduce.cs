using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class TransactionProduce : BaseEntity
    {
        public decimal Amount { get; set; }
        public double Quantity { get; set; }
        public string TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public string ProduceId { get; set; }
        public Produce Produce { get; set; }

    }
}