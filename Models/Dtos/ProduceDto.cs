using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Dtos
{
   public class ProduceDto
    {
        public string Id { get; set; }
        public string ProduceName { get; set; }
        public string CategoryId { get; set; }
        public double QuantityAvailable { get; set; }
        public decimal Price { get; set; }
        public string UnitOfMeasurement { get; set; }
         public string NameOfCategory {get;set;}
        public string DescriptionOfCategory {get;set;}
        
        public List<TransactionProduceDto> TransactionProduces { get; set; }
    }
    public class CreateProduceRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        public string ProduceName { get; set; }
        [Required]
        public double QuantityAvailable { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string UnitOfMeasurement { get; set; }
        [Required]
         public string NameOfCategory {get;set;}
        
    }
    public class UpdateProduceRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        public string ProduceName { get; set; }
        [Required]
        public double QuantityAvailable { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string UnitOfMeasurement { get; set; }
         [Required]
         public string NameOfCategory {get;set;}
    }
}