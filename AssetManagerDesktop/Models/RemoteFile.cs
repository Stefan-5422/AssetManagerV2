using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AssetManagerDesktop.Models
{
    public class RemoteFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [JsonPropertyName("name")]
        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        [JsonPropertyName("guid")]
        public string RemoteName
        {
            get => remoteName;
            set
            {
                remoteName = value;
                OnPropertyChanged(nameof(RemoteName));
            }
        }

        private string fileName = string.Empty;
        private string remoteName = string.Empty;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}