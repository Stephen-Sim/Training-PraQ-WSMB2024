using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditItemPrice : ContentPage
    {
        public ApiService apiService { get; set; }

        public ItemPrice ItemPrice { get; set; }

        public string ItemName { get; set; }

        public EditItemPrice(ItemPrice itemPrice, string itemName)
        {
            InitializeComponent();

            this.ItemPrice = itemPrice;
            ItemName = itemName;

            apiService = new ApiService();

            loadData();
        }

        public List<Rule> Rules { get; set; }

        async void loadData()
        {
            dateLabel.Text = this.ItemPrice.Date;
            this.Title = $"Edit - {ItemName}";
            priceEntry.Text = this.ItemPrice.Price.ToString();


            Rules = await apiService.GetRules();
            
            rulePicker.ItemsSource = Rules;
            rulePicker.ItemDisplayBinding = new Binding("Name");

            var rule = Rules.First(x => x.Id == this.ItemPrice.RuleId);

            rulePicker.SelectedItem = rule;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (rulePicker.SelectedItem == null || string.IsNullOrEmpty(priceEntry.Text))
            {
                await DisplayAlert("", "all fields are required.", "ok");
                return;
            }

            var ruleId = ((Rule)rulePicker.SelectedItem).Id;

            var res = await apiService.EditItemPrice(this.ItemPrice.ID, decimal.Parse(priceEntry.Text), ruleId);

            if (res)
            {
                await DisplayAlert("", "item price is edited.", "ok");

                await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}