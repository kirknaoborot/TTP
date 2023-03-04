namespace TTP.Models
{
    public class GalleryViewModel
    {
        public GalleryViewModel(string path, string name, string description)
        {
            ImagePath = path;
            Name = name;
            Description = description;
        }

        public string ImagePath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
