using MobileApp.Services;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public ApiService apiService { get; set; }

        public MainPage()
        {
            InitializeComponent();
            apiService = new ApiService();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("", "All the fields are required.", "OK");
                return;
            }

            var res = await apiService.Login(usernameEntry.Text, passwordEntry.Text);

            if (res)
            {
                App.Current.MainPage = new NavigationPage(new ListingPage());
            }
            else
            {
                await DisplayAlert("", "login failed.", "OK");
            }
        }
    }
}
