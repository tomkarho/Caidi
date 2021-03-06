using System.IO;

namespace Caidi.App.Models
{
    public enum ConversionStatus
    {
        NotStarted,
        Started,
        Finished
    }
    
    public class VideoFile
    {
        public FileInfo File { get; set; }
        public ConversionStatus ConversionStatus { get; set; } = ConversionStatus.NotStarted;
    }
}