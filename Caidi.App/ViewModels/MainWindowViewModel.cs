using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Caidi.App.Models;
using DynamicData;
using FFMpegCore;

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
        
        private List<VideoFile> _files = new();
        public List<VideoFile> Files
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

            if (selectedFiles == null)
                return;
            
            var data  = new List<VideoFile>();
            foreach (var path in selectedFiles)
            {
                if (File.Exists(path))
                {
                    data.Add(new VideoFile
                    {
                        File = new FileInfo(path)
                    });
                }
            }
            
            SourceFolderPath = data.FirstOrDefault()?.File?.DirectoryName;
            Console.WriteLine($"Loaded {data.Count} files from {SourceFolderPath}");
            
            Files = data;
        }

        private async Task<string[]> GetFiles(Window window)
        {
            var dialog = FileManager.GetDialog(SourceFolderPath, Files?.FirstOrDefault()?.File.Name);
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
            var tasks = Files.Select(videoFile => Task.Run(() => ExtractAudio(videoFile))).ToList();
            Task.WhenAll(tasks).ContinueWith((completed) => Console.WriteLine("All files extracted"));
        }

        private void ExtractAudio(VideoFile videoFile)
        {
            Console.WriteLine($"Starting processing {videoFile.File.Name}");
            var mediaInfo = FFProbe.Analyse(videoFile.File.FullName);

            if (mediaInfo == null)
            {
                Console.WriteLine($"Invalid file {videoFile.File.Name}");
                return;
            }
                    
            var success = FFMpeg.ExtractAudio(mediaInfo.Path, $"{mediaInfo.Path}.mp3");

            if (success)
            {
                Console.WriteLine($"{videoFile.File.Name} converted");
            }
            else
            {
                Console.WriteLine($"{videoFile.File.Name} processing failed");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
