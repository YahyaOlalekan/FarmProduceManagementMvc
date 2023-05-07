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

        public List<TransactionDetailsRequestModel> Products { get; set; }
    }

    public class TransactionDetailsRequestModel
    {

        //[Required]
        //[Display(Name = "Name")]
        //public string ProduceName { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public double Quantity { get; set; }
        [Required]
        [Display(Name = "Produce")]
        public string ProduceId { get; set; }
        //[Required]
        //[Display(Name = "Unit Of Measurement")]
        //public string UnitOfMeasurement { get; set; }
    }
    public class UpdateTransactionRequestModel
    {
        [Required]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }
        [Required]
        [Display(Name = "Quantity Available")]
        public double QuantityAvailable { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
    }

}
