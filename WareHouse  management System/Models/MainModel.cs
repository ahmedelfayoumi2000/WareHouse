namespace WareHouse__management_System.Models
{
    public class MainModel
    {
        public MainModel()
        {
            this.Id = Guid.NewGuid();   
        }
        public Guid Id { get; set; }


    }
}
