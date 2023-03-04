namespace TTP.Models
{
    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
        }

        public ServiceViewModel(string path, string name, string price)
        {
            PathImage = path;
            Name = name;
            Price = price;
        }

        public string PathImage { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }
    }
}
