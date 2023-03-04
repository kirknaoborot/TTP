namespace TTP.Models
{
    public class CoachViewModel
    {
        public CoachViewModel()
        {
        }

        public CoachViewModel(string imagePath, string name, string description, string level)
        {
            ImagePath = imagePath;
            Name = name;
            Description = description;
            Level = level;
        }

        public string ImagePath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Level { get; set; }
    }
}
