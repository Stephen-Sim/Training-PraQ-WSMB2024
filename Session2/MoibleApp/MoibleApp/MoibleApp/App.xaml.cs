using MoibleApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoibleApp
{
    public partial class App : Application
    {
        public static User User { get; set; }

        public App()
        {
            InitializeComponent();

            User = new User();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
