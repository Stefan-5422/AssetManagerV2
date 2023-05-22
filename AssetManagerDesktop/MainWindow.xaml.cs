using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace AssetManagerDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary
    public partial class MainWindow : Window
    {
        private readonly LoginPage loginPage;
        private readonly MainPage mainPage;

        public MainWindow(LoginPage login, MainPage main)
        {
            InitializeComponent();
            login.LoginSuccess += Page_LoginSuccess;

            this.PageFrame.Child = login;
            this.mainPage = main;
            this.loginPage = login;
        }

        private async void Page_LoginSuccess(object? sender, EventArgs e)
        {
            this.PageFrame.Child = mainPage;
            await mainPage.InitializeContent();
        }
    }
}