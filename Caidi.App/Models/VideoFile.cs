using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Caidi.App.Models
{
    public enum ConversionStatus
    {
        NotStarted,
        Started,
        Success,
        Failure
    }
    
    public class VideoFile : INotifyPropertyChanged
    {
        public FileInfo File { get; set; }
        
        private ConversionStatus _conversionStatus = ConversionStatus.NotStarted;
        public ConversionStatus ConversionStatus
        {
            get => _conversionStatus;
            set
            {
                _conversionStatus = value;
                OnPropertyChanged();
            }
        }
        
        
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}