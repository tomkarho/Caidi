using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;

namespace Caidi.App.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly List<string> MovieExtensions = new()
        {
            "webm",
            "mpg",
            "mp2",
            "mpeg",
            "mpe",
            "mpv",
            "ogg",
            "mp4",
            "m4p",
            "m4v",
            "avi",
            "wmv",
            "mov",
            "qt",
            "flv",
            "swfavchd"
        };

        private string _sourceFolderPath;
        public string SourceFolderPath
        {
            get => _sourceFolderPath;
            set
            {
                _sourceFolderPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceFolderPath)));
            } }
        
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
            var dialog = GetDialog();

            var selectedFiles = await dialog.ShowAsync(window);
            if (selectedFiles == null)
            {
                Console.WriteLine("File dialog cancelled by user");
                return;
            }
            
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

        private OpenFileDialog GetDialog()
        {
            return new()
            {
                Title = "Choose video files",
                AllowMultiple = true,
                Filters = new List<FileDialogFilter>
                {
                    // Todo: mimetype would be more appropriate way to determine which is a video file
                    new() {Name = "Videos", Extensions = MovieExtensions},
                },
                Directory = SourceFolderPath,
                InitialFileName = Files?.FirstOrDefault()?.Name
            };
        }

        public void OnExtractAudioClick()
        {
            Console.WriteLine("OnExtractAudioClick clicked");
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
