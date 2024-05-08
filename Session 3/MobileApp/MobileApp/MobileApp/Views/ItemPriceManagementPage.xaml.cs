using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPriceManagementPage : ContentPage
    {
        public ApiService apiService { get; set; }
        
        public Listing Item { get; set; }

        public ItemPriceManagementPage(Listing Item)
        {
            InitializeComponent();

            apiService = new ApiService();
            this.Item = Item;

            this.Title = $"{Item.Name} Prices";

            loadData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            loadData();
        }

        async void loadData()
        {
            var res = await apiService.GetItemPrices(this.Item.ID);
            lv.ItemsSource = res;

            var res1 = await apiService.GetRules();
            weekendRulePicker.ItemsSource = res1;
            weekendRulePicker.ItemDisplayBinding = new Binding("Name");

            holidayRulePicker.ItemsSource = res1;
            holidayRulePicker.ItemDisplayBinding = new Binding("Name");

            otherDayRulePicker.ItemsSource = res1;
            otherDayRulePicker.ItemDisplayBinding = new Binding("Name");
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            // swipe right
            var ip = (sender as SwipeItem).CommandParameter as ItemPrice;

            if (ip.Status == "Booked")
            {
                await DisplayAlert("", "item price is booked", "ok");
                return;
            }

            await App.Current.MainPage.Navigation.PushAsync(new EditItemPrice(ip, this.Item.Name));
        }

        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            // swipe left
            var ip = (sender as SwipeItem).CommandParameter as ItemPrice;

            if (ip.Status == "Booked")
            {
                await DisplayAlert("", "item price is booked", "ok");
                return;
            }

            var res = await DisplayAlert("", "are you sure to delete this item price", "yes", "no");

            if (res)
            {
                var res1 = await apiService.DeleteItemPrice(ip.ID);

                if (res1)
                {
                    await DisplayAlert("", "item price is deleted", "ok");
                    loadData();
                }
                else
                {
                    await DisplayAlert("", "fail to delete", "ok");
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (startDatePicker.Date < DateTime.Today || startDatePicker.Date > DateTime.Today.AddDays(90))
            {
                await DisplayAlert("", "Invalid date. the date should be today or onward but not greater than 90 days", "Ok");
                return;
            }

            if (startDatePicker.Date > endDatePicker.Date)
            {
                await DisplayAlert("", "Invalid date. the start date could not greater than end date.", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(otherDayPriceEntry.Text) || otherDayRulePicker.SelectedItem == null)
            {
                await DisplayAlert("", "Invalid date. other day fields are mandetory.", "Ok");
                return;
            }

            var addItemPriceDto = new AddItemPriceDTO()
            {
                StartDate = startDatePicker.Date,
                EndDate = endDatePicker.Date,
                ItemID = this.Item.ID,
                WeekendPrice = string.IsNullOrEmpty(weekendPriceEntry.Text) ? decimal.Parse(otherDayPriceEntry.Text) : decimal.Parse(weekendPriceEntry.Text),
                WeekendRuleId = weekendRulePicker.SelectedItem == null ? ((Rule)otherDayRulePicker.SelectedItem).Id : ((Rule)weekendRulePicker.SelectedItem).Id,

                HolidayPrice = string.IsNullOrEmpty(holidayPriceEntry.Text) ? decimal.Parse(otherDayPriceEntry.Text) : decimal.Parse(holidayPriceEntry.Text),
                HolidayRuleId = holidayRulePicker.SelectedItem == null ? ((Rule)otherDayRulePicker.SelectedItem).Id : ((Rule)holidayRulePicker.SelectedItem).Id,

                OtherDayPrice = decimal.Parse(otherDayPriceEntry.Text),
                OtherDayRuleId = ((Rule)otherDayRulePicker.SelectedItem).Id,
            };

            var res = await apiService.AddItemPrice(addItemPriceDto);

            if (res)
            {
                await DisplayAlert("", "item price is added", "Ok");
                loadData();
            }
            else
            {
                await DisplayAlert("", "failed", "Ok");
            }
        }
    }
}