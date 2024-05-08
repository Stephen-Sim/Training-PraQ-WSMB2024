using MoibleApp.Services;
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
        public ValuesService valuesService { get; set; }

        public MainPage()
        {
            InitializeComponent();

            valuesService = new ValuesService();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Alert", "All fields are required.", "ok");
                return;
            }

            var res = await valuesService.Login(usernameEntry.Text, passwordEntry.Text);

            if (res)
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
