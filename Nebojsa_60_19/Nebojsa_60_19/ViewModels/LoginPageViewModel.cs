using Nebojsa_60_19.Models;
using Nebojsa_60_19.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nebojsa_60_19.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private string loginError;

        public string LoginError
        {
            get { return loginError; }
            set => SetProperty(ref loginError, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand LoginCommand { get; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(LoginUser);
        }

        private async void LoginUser()
        {
            var client = new RestClient();

            var request = new RestRequest("https://10.0.2.2:5011/api/Token");
            request.Method = Method.POST;
            request.AddJsonBody(this);

            var response = client.Execute<LoginResponse>(request);


            if(response.IsSuccessful)
            {
                Application.Current.Properties["UserData"] = response.Data;
                await AppShell.Current.GoToAsync("//"+nameof(GenresPage));

            } else
            {
                this.LoginError = "The credentials are not correct";
            }
        }
    }
}
