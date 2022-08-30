using Nebojsa_60_19.ViewModels;
using Nebojsa_60_19.Views;
using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace Nebojsa_60_19
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(GenresPage), typeof(GenresPage));
            Routing.RegisterRoute(nameof(AlbumsPage), typeof(AlbumsPage));
            Routing.RegisterRoute(nameof(InstrumentsPage), typeof(InstrumentsPage));
            Routing.RegisterRoute(nameof(AddInstrumentPage), typeof(AddInstrumentPage));
            Routing.RegisterRoute(nameof(AddTypePage), typeof(AddTypePage));
            Routing.RegisterRoute(nameof(EditInstrumentPage), typeof(EditInstrumentPage));



            ServicePointManager.ServerCertificateValidationCallback += (x, y, z, w) => true;
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
