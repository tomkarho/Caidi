using System.IO;

namespace Caidi.App.Models
{
    public class SourceFileDto
    {
        public string DisplayLabel { get; }
        public FileInfo File { get; set; }
        public bool Checked { get; set; }

        public SourceFileDto(FileInfo file)
        {
            File = file;

            if (file.Name.Length > 50)
            {
                DisplayLabel = $"{file.Name.Substring(0, 47)}...";   
            }
            else
            {
                DisplayLabel = file.Name;
            }
        }
    }
}