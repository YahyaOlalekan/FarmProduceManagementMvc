using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class Admin : BaseEntity
    {
        public decimal CompanyWallet { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}