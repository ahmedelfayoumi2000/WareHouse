using System.ComponentModel.DataAnnotations;

namespace WareHouse__management_System.View_Model
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
