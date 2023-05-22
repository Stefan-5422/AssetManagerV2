using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace AssetManagerDesktop
{
    /// <summary>
    /// Interaction logic for RemoteFile.xaml
    /// </summary>
    public partial class RemoteFile : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [JsonPropertyName("name")]
        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged();
            }
        }

        [JsonPropertyName("guid")]
        public string RemoteName
        {
            get => guid;
            set
            {
                guid = value;
                OnPropertyChanged();
            }
        }

        private string fileName = String.Empty;
        private string guid = String.Empty;

        public RemoteFile()
        {
            DataContext = this;
            InitializeComponent();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}