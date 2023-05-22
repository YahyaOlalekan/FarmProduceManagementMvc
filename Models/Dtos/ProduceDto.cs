using FarmProduceManagement.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public double QuantityToBuy { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string NameOfCategory { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public string DescriptionOfCategory { get; set; }

        public List<TransactionProduceDto> TransactionProduces { get; set; }
    }
    public class CreateProduceRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }

        [Required]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Required]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

    }


    public class PurchaseProduceRequestModel
    {
        // [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        // public SelectList CategoryList { get; set; }

        [Required]
        [Display(Name = "Produce")]
        public List<string> ProduceId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public List<double> QuantityToBuy { get; set; }

        // public string UnitOfMeasurement { get; set; }
    }


    public class UpdateProduceRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public double QuantityToBuy { get; set; }
        
        [Required]
         [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

         [Required]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        // [Required]
        // [Display(Name = "Name of Category")]
        // public string NameOfCategory { get; set; }
    }
}