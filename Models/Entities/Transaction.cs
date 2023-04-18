using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class Transaction : BaseEntity
    {
        public string TransactionNum {get;set;}
        public string FarmerId { get; set; }
        public Farmer Farmer { get; set; }
        public string ManagerId { get; set; }
        public Manager Manager { get; set; }
        public ICollection<TransactionProduce> TransactionProduces { get; set; } = new HashSet<TransactionProduce>();

    }
}