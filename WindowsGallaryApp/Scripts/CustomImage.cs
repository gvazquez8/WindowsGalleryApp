using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace WindowsGallaryApp.Scripts
{
    class CustomImage
    {
        private static Random rand = new Random();
        private static List<string> titles = new List<string>()
        { "Cliffs", "Grapes", "Landscape1", "Landscape2", "Landscape3",
          "Landscape4", "Landscape5", "Landscape6", "Landscape7",
          "Landscape8", "Rainier", "Sunset", "Treetops", "Valley" };

        private static List<string> filenames = new List<string>()
        { "cliff.jpg", "grapes.jpg", "LandscapeImage1.jpg", "LandscapeImage2.jpg",
          "LandscapeImage3.jpg", "LandscapeImage4.jpg", "LandscapeImage5.jpg",
          "LandscapeImage6.jpg", "LandscapeImage7.jpg", "LandscapeImage8.jpg",
          "rainier.jpg", "sunset.jpg", "treetops.jpg", "valley.jpg" };

        private static List<string> desc = new List<string>()
        { "A photo of cliffs.", "A photo of grapes.", "A photo of landscapes", "A photo of landscapes",
          "A photo of landscapes", "A photo of landscapes", "A photo of landscapes",
          "A photo of landscapes", "A photo of landscapes", "A photo of landscapes",
          "A photo of rainier.", "A sunset photo.", "A photo of treetops.", "A photo of a valley"};
        
        public string Title { get; set; }
        public string ImageLocation { get; set; }
        public string Description { get; set; }

        public CustomImage(string Title, string ImageLocation, string Description = "")
        {
            this.Title = Title;
            this.ImageLocation = ImageLocation;
            this.Description = Description;
        }

        private static CustomImage GenerateImage()
        {
            int index = rand.Next(0, filenames.Count);
            return new CustomImage(titles[index], @"/Assets/Media/" + filenames[index], desc[index]);
        }
        public static List<CustomImage> GetImages(int numImages)
        {
            List<CustomImage> images = new List<CustomImage>();

            for (int i = 0; i < numImages; i++)
            {
                images.Add(GenerateImage());
            }
            return images;
        }
    }
}
