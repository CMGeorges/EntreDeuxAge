using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEntre2Ages.Services;
using XamarinEntre2Ages.Views;

namespace XamarinEntre2Ages
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
