using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssetManagerDesktop.Services.Implementations;

namespace AssetManagerDesktop
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl, INotifyPropertyChanged
    {
        public event EventHandler LoginSuccess;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string ServerAddress
        {
            get => userConfigProvider.Config.ServerName;
            set => userConfigProvider.Config.ServerName = value;
        }
        public string Username
        {
            get => userConfigProvider.Config.UserName;
            set => userConfigProvider.Config.UserName = value;
        }

        public string Password { get; set; }
        private readonly HttpClient httpClient;
        private readonly UserConfigProvider userConfigProvider;

        public LoginPage(HttpClient client, UserConfigProvider userConfigProvider)
        {
            httpClient = client;
            this.userConfigProvider = userConfigProvider;

            userConfigProvider.UserConfigUpdated += (caller, args) =>
            {
                if (ReferenceEquals(caller, this)) return;
                OnPropertyChanged();
            };

            InitializeComponent();
            DataContext = this;
        }

        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, string> payload = new()
                {
                    { "name", Username },
                    { "password", Password }
                };

                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"http://{ServerAddress}/User/Login", payload);

                //Debug.WriteLine(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    userConfigProvider.Config.ApiToken = await response.Content.ReadAsStringAsync();
                    LoginSuccess.Invoke(this, EventArgs.Empty);
                }

            }
            catch (HttpRequestException httpRequestException)
            {
                Debug.WriteLine(httpRequestException.Message);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.StackTrace);
            }

        }
        private async void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, string> payload = new()
                {
                    { "name", Username },
                    { "password", Password }
                };
                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"http://{ServerAddress}/User/Register", payload);

                Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException httpRequestException) //TODO: Give rich feedback to the user;
            {
                Debug.WriteLine(httpRequestException.Message);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.StackTrace);
            }
        }

        private void ChangeServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeServerWindow.Show();
        }

        private void ChangeServerSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            ServerAddress = ChangeServerTextBox.Text;
            userConfigProvider.Updated();
            OnPropertyChanged("ServerAddress");
            //TODO: Check if Server is valid
            ChangeServerWindow.Close();
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
