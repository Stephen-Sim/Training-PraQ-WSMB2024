using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListingPage : ContentPage
    {
        public ApiService apiService { get; set; }

        public ListingPage()
        {
            InitializeComponent();
            apiService = new ApiService();

            loadData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loadData();
        }

        async void loadData()
        {
            var res = await apiService.GetListings();
            lv.ItemsSource = res;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var item = (sender as Button).CommandParameter as Listing;
            App.Current.MainPage.Navigation.PushAsync(new ItemPriceManagementPage(item));
        }
    }
}