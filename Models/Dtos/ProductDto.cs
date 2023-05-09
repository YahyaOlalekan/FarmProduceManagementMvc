using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string ProduceName { get; set; }
        public string CategoryId { get; set; }
        public double QuantityToSell { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string NameOfCategory { get; set; }

        public string DescriptionOfCategory { get; set; }

        public List<TransactionProduceDto> TransactionProduces { get; set; }
    }
    public class CreateProductRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal SellingPrice { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

    }


    public class BuyProductRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public SelectList CategoryList { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Produce")]
        public string ProduceId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public double QuantityToSell { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal SellingPrice { get; set; }
        // [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        public bool IsAvailable { get; set; }
        // [Required]
        // [Display(Name = "Name of Category")]
        // public string NameOfCategory { get; set; }
    }

    public class UpdateProductRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public double QuantityToSell { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal SellingPrice { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        // [Required]
        // [Display(Name = "Name of Category")]
        // public string NameOfCategory { get; set; }
    }
}