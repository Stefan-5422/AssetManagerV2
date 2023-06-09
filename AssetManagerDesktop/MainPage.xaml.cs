﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssetManagerDesktop.Models;
using AssetManagerDesktop.Services;
using AssetManagerDesktop.Services.Implementations;
using Microsoft.Win32;

namespace AssetManagerDesktop
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Models.RemoteFile> Files
        {
            get => remoteFiles;
            set
            {
                remoteFiles = value;
                OnPropertyChanged();
            }
        }

        private readonly IRemoteFileService remoteFileService;
        private ObservableCollection<Models.RemoteFile> remoteFiles = new();

        public MainPage(UserConfigProvider userConfigProvider, IRemoteFileService remoteFileService)
        {
            this.remoteFileService = remoteFileService;

            DataContext = this;
            InitializeComponent();
        }

        public async Task InitializeContent()
        {
            Files = new ObservableCollection<Models.RemoteFile>(await remoteFileService.GetRemoteFiles());
            OnPropertyChanged(nameof(Files));
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

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonBase? button = sender as ButtonBase;

            string remoteFileName = (button?.DataContext as RemoteFile)?.RemoteName ?? "";

            remoteFileService.DownloadRemoteFile(remoteFileName);
        }

        private async void ReloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            Files = new ObservableCollection<Models.RemoteFile>(await remoteFileService.GetRemoteFiles());
        }

        private async void SelectFilesButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == true)
            {
                List<string> files = dialog.FileNames.ToList();
                await remoteFileService.SendRemoteFiles(files);
                await InitializeContent();
            }
        }
    }
}