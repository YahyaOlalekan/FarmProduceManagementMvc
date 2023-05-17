using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Models.Entities
{
    public class Transaction : BaseEntity
    {
        public string TransactionNum {get;set;}
        public string FarmerId { get; set; }
        public Farmer Farmer { get; set; }
        public ICollection<TransactionProduce> TransactionProduces { get; set; } = new HashSet<TransactionProduce>();
        public TransactionStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public double TotalQuantity { get; set; }
    }
}