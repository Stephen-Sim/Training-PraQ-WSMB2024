using MoibleApp.Models;
using MoibleApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoibleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserPage : ContentPage
    {
        public ValuesService valuesService { get; set; }
        public AddUserPage()
        {
            InitializeComponent();

            valuesService = new ValuesService();

            maleRadioButton.IsChecked = true;
            loadData();
        }

        async void loadData()
        {
            var res = await valuesService.GetRoles();

            rolePicker.ItemsSource = res;
            rolePicker.ItemDisplayBinding = new Binding("Name");
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            var user = new User()
            {
                Username = usernameEntry.Text,
                FirstName = firstNameEntry.Text,
                LastName = lastNameEntry.Text,
                Age = int.Parse(ageEntry.Text),
                Gender = maleRadioButton.IsChecked,
                RoleID = (rolePicker.SelectedItem as Role).ID,
            };

            var res = await valuesService.StoreUser(user);

            if (res)
            {
                await App.Current.MainPage.Navigation.PushAsync(new UserListPage());
            }
            else
            {
                await DisplayAlert("", "add failed", "ok");
            }
        }

        private async void Button_Clicked_1(object sender, System.EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}