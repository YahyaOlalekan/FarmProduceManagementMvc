using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class Manager : BaseEntity
    {
       
        public string RegistrationNumber { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
  
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
         public ICollection<FarmerManager> FarmerManagers { get; set; } = new HashSet<FarmerManager>();

         
    }
}