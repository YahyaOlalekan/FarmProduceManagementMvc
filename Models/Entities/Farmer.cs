using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Models.Entities
{
    public class Farmer : BaseEntity
    {
       
        public string RegistrationNumber { get; set; } 
        public FarmerRegStatus FarmerRegStatus { get; set; } = FarmerRegStatus.Pending;
        public decimal Wallet { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
   
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        
    }
}