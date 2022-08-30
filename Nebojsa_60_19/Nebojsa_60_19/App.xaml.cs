using Nebojsa_60_19.Services;
using Nebojsa_60_19.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nebojsa_60_19
{
    public partial class App : Application
    {
        public Xamarin.Forms.ImageSource Icon { get; set; }
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
