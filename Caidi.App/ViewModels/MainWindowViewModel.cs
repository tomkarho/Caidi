using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Caidi.App.Models;

namespace Caidi.App.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _sourceFolderPath;
        public string SourceFolderPath
        {
            get => _sourceFolderPath;
            set
            {
                _sourceFolderPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceFolderPath)));
            }
        }
        
        private List<FileInfo> _files = new();
        public List<FileInfo> Files
        {
            get => _files;
            set
            {
                _files = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Files)));
            }
        }

        public async void OnLoadFilesClick(Window window)
        {
            var selectedFiles = await GetFiles(window);

            var data  = new List<FileInfo>();
            foreach (var path in selectedFiles)
            {
                if (File.Exists(path))
                {
                    data.Add(new FileInfo(path));
                }
            }
            
            SourceFolderPath = data.FirstOrDefault()?.DirectoryName;
            Console.WriteLine($"Loaded {data.Count} files from {SourceFolderPath}");
            
            Files = data;
        }

        private async Task<string[]> GetFiles(Window window)
        {
            var dialog = FileManager.GetDialog(SourceFolderPath, Files?.FirstOrDefault()?.Name);
            var selectedFiles = await dialog.ShowAsync(window);
            if (selectedFiles == null)
            {
                Console.WriteLine("File dialog cancelled by user");
                return null;
            }

            if (!selectedFiles.Any())
            {
                Console.WriteLine("No files where selected");
                return null;
            }

            return selectedFiles;
        }

        public void OnExtractAudioClick()
        {
            Console.WriteLine("OnExtractAudioClick clicked");
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
