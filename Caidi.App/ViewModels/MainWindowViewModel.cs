using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace Caidi.App.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string SourceFolderPath => "";
        
        private List<FileInfo> files = new List<FileInfo>();
        public List<FileInfo> Files
        {
            get => files;
            set
            {
                files = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Files)));
            }
        }

        public void OnLoadFilesClick()
        {
            Console.WriteLine($"Getting files from {SourceFolderPath}");
            var filePaths = Directory.GetFiles(SourceFolderPath);

            // Reset files list when files are found
            if (!filePaths.Any())
            {
                Console.WriteLine($"No files found in location '{SourceFolderPath}'");
                return;
            }
            
            var data  = new List<FileInfo>();
            foreach (var path in filePaths)
            {
                if (File.Exists(path))
                {
                    data.Add(new FileInfo(path));
                }
            }

            Files = data;
        }
        public void OnExtractAudioClick() => Console.WriteLine("OnExtractAudioClick clicked");
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
