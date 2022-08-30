using Nebojsa_60_19.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Nebojsa_60_19.Views;

namespace Nebojsa_60_19.ViewModels
{
    public class GenresPageViewModel : BaseViewModel
    {
        private ICollection<GenresDTO> genres;
        private string responseText;
        private string responseColor;
        private bool isVisible = false;
        private string searchGenres;
        private bool isRefreshing;

        public bool IsRefreshing { get => isRefreshing; set => SetProperty(ref isRefreshing, value); }
        public ICollection<GenresDTO> Genres { get => genres; set => SetProperty(ref genres, value); }
        public string ResponseText { get => responseText; set => SetProperty(ref responseText, value); }
        public string ResponseColor { get => responseColor; set => SetProperty(ref responseColor, value); }
        public bool IsVisible { get => isVisible; set => SetProperty(ref isVisible, value); }
        public string SearchGenres
        {
            get => searchGenres;
            set
            {
                SetProperty(ref searchGenres, value);
                var client = new RestClient();
                var request = new RestRequest("https://10.0.2.2:5011/api/Genres?Name=" + SearchGenres);
                request.Method = Method.GET;

                var loginResponse = Application.Current.Properties["UserData"] as LoginResponse;

                request.AddHeader("Authorization", "Bearer " + loginResponse.Token);

                var response = client.Execute<IEnumerable<GenresDTO>>(request);

                Genres = new ObservableCollection<GenresDTO>(response.Data);
            }
        }
        public GenresPageViewModel()
        {
            LoadPage();
            DeleteCommand = new Command(DeleteGenre);
            HideCommand = new Command(() =>
            {
                IsVisible = false;
            });
            RefreshCommand = new Command(LoadPage);
        }

        private void DeleteGenre(object item)
        {
            var genre = item as GenresDTO;

            var client = new RestClient();
            var request = new RestRequest("https://10.0.2.2:5011/api/Genres/" + genre.Id);
            request.Method = Method.DELETE;
            var loginResponse = Application.Current.Properties["UserData"] as LoginResponse;
            request.AddHeader("Authorization", "Bearer" + loginResponse.Token);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Genres.Remove(genre);
                ResponseText = "Successful delete";
                ResponseColor = "Green";
                IsVisible = true;
            }
            else
            {
                ResponseText = "Deletion failed";
                ResponseColor = "Red";
                IsVisible = true;
            }

        }

        public ICommand DeleteCommand { get; }

        public ICommand HideCommand { get; }

        private void LoadPage()
        {
            var client = new RestClient();
            var request = new RestRequest("https://10.0.2.2:5011/api/Genres");
            request.Method = Method.GET;

            var loginResponse = Application.Current.Properties["UserData"] as LoginResponse;

            request.AddHeader("Authorization", "Bearer " + loginResponse.Token);

            var response = client.Execute<IEnumerable<GenresDTO>>(request);

            Genres = new ObservableCollection<GenresDTO>(response.Data);
            IsRefreshing = false;
        }
        public ICommand RefreshCommand { get; }

    }
}
