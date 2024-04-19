using MoibleApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoibleApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Alert", "All fields are required.", "ok");
                return;
            }

            if (usernameEntry.Text == "Alex" && passwordEntry.Text == "abc123")
            {
                await App.Current.MainPage.Navigation.PushAsync(new UserListPage());
            }
            else
            {
                await DisplayAlert("Alert", "user not found.", "ok");
            }
        }
    }
}
