using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace Caidi.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string FilesPath => "/home/tomkarho/Downloads/YT";

        public void OnLoadFilesClick() => Console.WriteLine("Load files clicked");
        public void OnExtractAudioClick() => Console.WriteLine("OnExtractAudioClick clicked");
    }
}
