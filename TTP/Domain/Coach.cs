using System.ComponentModel.DataAnnotations.Schema;

namespace TTP.Domain
{
    [Table("COACH")]
    public class Coach
    {
        public Coach()
        {
        }

        public Coach(string imagePath, string name, string description, string level)
        {
            ImagePath = imagePath;
            Name = name;
            Description = description;
            Level = level;
        }

        [Column("ID")]
        public int Id { get; set; }

        [Column("IMAGE_PATH")]
        public string ImagePath { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("LEVEL")]
        public string Level { get; set; }
    }
}
