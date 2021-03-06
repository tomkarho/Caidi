using System.Collections.Generic;
using Avalonia.Controls;

namespace Caidi.App.Models
{
    public class FileManager
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
        
        public static  OpenFileDialog GetDialog(string directoryPath, string initialFileName)
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
                Directory = directoryPath,
                InitialFileName = initialFileName
            };
        }
    }
}