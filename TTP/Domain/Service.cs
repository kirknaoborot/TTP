using System.ComponentModel.DataAnnotations.Schema;

namespace TTP.Domain
{
    [Table("SERVICES")]
    public class Service
    {
        public Service()
        {
        }

        public Service(string imagePath, string name, string price)
        {
            Name = name;
            ImagePath = imagePath;
            Price = price;
        }

        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("IMAGE_PATH")]
        public string ImagePath { get; set; }

        [Column("PRICE")]
        public string Price { get; set; }
    }
}
