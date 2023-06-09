using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Models.Dtos
{
    public class OrderProductDto
    {
        public string Id { get; set; }
        public string ProduceId { get; set; }
        public Produce Produce { get; set; }
        public string OrderId { get; set; }
         public string OrderNumber {get;set;}
         public double Quantity {get;set;}
         public decimal Price {get;set;}
    }
}