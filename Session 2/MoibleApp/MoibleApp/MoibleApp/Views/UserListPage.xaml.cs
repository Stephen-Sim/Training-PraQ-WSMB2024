using MoibleApp.Models;
using MoibleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoibleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserListPage : ContentPage
    {
        public UserListPage()
        {
            InitializeComponent();

            valuesService = new ValuesService();

            loadData();
        }

        public ValuesService valuesService { get; set; }

        async void loadData()
        {
            usernameLabel.Text = $"Welcome, {App.User.FullName}";

            var res = await valuesService.GetUsers();

            lv.ItemsSource = res;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            // delete 

            var user = (sender as ImageButton).CommandParameter as User;

            var res = await valuesService.DeleteUser((long)user.ID);

            if (res)
            {
                await DisplayAlert("", "user is deleted.", "ok");

                loadData();
            }
            else
            {
                await DisplayAlert("", "fail to delete.", "ok");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // go to add user page
            await App.Current.MainPage.Navigation.PushAsync(new AddUserPage());
        }
    }
}