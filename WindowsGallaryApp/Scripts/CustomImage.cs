using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace WindowsGallaryApp.Scripts
{
    class CustomImage
    {
        public string Title { get; set; }
        public string DateCreated { get; set; }
        public string Type { get; set; }
        public string ImageLocation { get; set; }

        public CustomImage(string Title, string ImageLocation, string fileType, DateTimeOffset DateCreated)
        {
            var lastDash = Title.LastIndexOf('-');
            this.Title = Title.Substring(0, lastDash);

            this.ImageLocation = ImageLocation;

            this.Type = fileType;

            this.DateCreated = DateCreated.UtcDateTime.ToString();
        }

        public static async Task<List<CustomImage>> GenerateImages()
        {
            StorageFolder mediaFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var files = await mediaFolder.GetFilesAsync();

            List<CustomImage> images = new List<CustomImage>();

            foreach (var file in files)
            {
                images.Add(new CustomImage(file.DisplayName, file.Path, file.FileType, file.DateCreated));    
            }

            return images;
        }


    }
}
