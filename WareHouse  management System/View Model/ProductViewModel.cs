using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WareHouse__management_System.Models;

namespace WareHouse__management_System.View_Model
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(60, ErrorMessage = "Max Length should not be more than 60 characters")]
        [MinLength(3, ErrorMessage = "product name should be at least 3 characters")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "Description should not be more than 100 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 999999, ErrorMessage = "Can not be less than 0 and more than 999999")]
        public decimal Cost { get; set; }
        [Required]
        [Range(0, 999999, ErrorMessage = "Can not be less than 0 and more than 999999")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Can not be less than 0 and more than 10000")]
        public int Count { get; set; }
        public string ImageURL { get; set; }
        public string CategoryId { get; set; }
        public IEnumerable<SelectListItem> ?Categories { get; set; }
    }
}