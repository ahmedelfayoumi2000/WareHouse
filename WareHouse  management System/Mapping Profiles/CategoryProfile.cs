using AutoMapper;
using WareHouse__management_System.Models;
using WareHouse__management_System.View_Model;

namespace WareHouse__management_System.Mapping_Profiles
{
    public class CategoryProfile :Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap(); 
        }
    }
}
