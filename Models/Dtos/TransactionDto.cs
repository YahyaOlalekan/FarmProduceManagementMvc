using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Dtos
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public string TransactionNum { get; set; }
        public string FarmerId { get; set; }
        public string ManagerId { get; set; }
        public List<ManagerDto> Managers { get; set; }
        public List<FarmerDto> Farmers { get; set; }
        public List<TransactionProduceDto> TransactionProduces { get; set; }
        public string ProduceName { get; set; }
        public double QuantityAvailable { get; set; }
        public decimal Price { get; set; }
        public string UnitOfMeasurement { get; set; }
    }

    public class CreateTransactionRequestModel
    {
        [Required]
        public string TransactionNum { get; set; }
         [Required]
        public string ProduceName { get; set; }
         [Required]
        public double QuantityAvailable { get; set; }
         [Required]
        public decimal Price { get; set; }
         [Required]
        public string UnitOfMeasurement { get; set; }
    }
    public class UpdateTransactionRequestModel
    {
         [Required]
        public string ProduceName { get; set; }
         [Required]
        public double QuantityAvailable { get; set; }
         [Required]
        public string UnitOfMeasurement { get; set; }
    }

}
