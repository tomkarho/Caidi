using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
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
        private string _assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
        public FileInfo File { get; set; }

        public Bitmap StatusIcon
        {
            get
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                var fileName = "";

                switch (ConversionStatus)
                {
                    case ConversionStatus.Failure:
                        fileName = "nok";
                        break;
                    case ConversionStatus.Started:
                        fileName = "loading";
                        break;
                    case ConversionStatus.Success:
                        fileName = "ok";
                        break;
                    default:
                        break;
                }
                
                return new Bitmap(assets.Open(new Uri($"avares://{_assemblyName}/Assets/{fileName}.png")));
            }
        }

        private ConversionStatus _conversionStatus = ConversionStatus.NotStarted;
        public ConversionStatus ConversionStatus
        {
            get => _conversionStatus;
            set
            {
                _conversionStatus = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StatusIcon));
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