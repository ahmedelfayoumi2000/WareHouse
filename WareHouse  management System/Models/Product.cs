using System.ComponentModel.DataAnnotations;

namespace WareHouse__management_System.Models
{
    public class Product : MainModel
    {


        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
        public string ImageURL { get; set; }
        public Guid CategoryId { get; set; }
        public Category? category { get; set; }
    }
}
