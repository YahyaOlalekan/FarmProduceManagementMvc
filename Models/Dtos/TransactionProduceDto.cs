using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Models.Dtos
{
    public class TransactionProduceDto
    {
        public string FarmerId { get; set; }
        public string ManagerId { get; set; }
        public string TransactionId { get; set; }
         public string TransactionNum {get;set;}
        public List<TransactionProduceDto> TransactionProduces { get; set; }
    }
}