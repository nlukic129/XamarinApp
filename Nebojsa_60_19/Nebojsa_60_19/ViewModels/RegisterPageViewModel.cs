﻿using Nebojsa_60_19.Validators;
using Nebojsa_60_19.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nebojsa_60_19.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string password;

        private string firstNameError;
        private string lastNameError;
        private string emailError;
        private string usernameError;
        private string passwordError;

        private bool formValid = false;

        private string registerError;

        public string FirstName 
        { 
            get => firstName; 
            set
            {
                SetProperty(ref firstName, value);
                FirstNameError = Validator("FirstName");
            }
        }
        public string LastName 
        { 
            get => lastName; 
            set
            {
                SetProperty(ref lastName, value);
                LastNameError = Validator("LastName");
            }
        }
        public string Email 
        { 
            get => email; 
            set
            {
                SetProperty(ref email, value);
                EmailError = Validator("Email");
            }
        }
        public string Username 
        { 
            get => username; 
            set
            {
                SetProperty(ref username, value);
                UsernameError = Validator("Username");
            }
        }
        public string Password 
        { 
            get => password;
            set
            {
                SetProperty(ref password, value);
                PasswordError = Validator("Password");
            }
        }
        public string FirstNameError { get => firstNameError; set => SetProperty(ref firstNameError, value); }
        public string LastNameError { get => lastNameError; set => SetProperty(ref lastNameError, value); }
        public string EmailError { get => emailError; set => SetProperty(ref emailError, value); }
        public string UsernameError { get => usernameError; set => SetProperty(ref usernameError, value); }
        public string PasswordError { get => passwordError; set => SetProperty(ref passwordError, value); }
        public bool FormValid { get => formValid; set => SetProperty(ref formValid, value); }
        public string RegisterError { get => registerError; set => SetProperty(ref registerError, value); }

        private string Validator(string property)
        {
            var valitation = new RegistrationPageViewValidator();
            var result = valitation.Validate(this);

            FormValid = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }

        public ICommand RegisterCommand { get; }
        

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(RegisterUser);
        }

        private async void RegisterUser()
        {
            var client = new RestClient();
            var request = new RestRequest("https://10.0.2.2:5011/api/Register");
            request.Method = Method.POST;
            request.AddJsonBody(this);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                RegisterError = response.ErrorMessage;
            }

        }
    }
}
