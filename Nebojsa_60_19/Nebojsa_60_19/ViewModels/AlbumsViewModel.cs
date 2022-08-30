using Nebojsa_60_19.Models;
using Nebojsa_60_19.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nebojsa_60_19.ViewModels
{
    public class AlbumsViewModel : BaseViewModel
    {
        private bool isRefreshing;
        private bool isVisible = false;
        private string responseText;
        private string responseColor;
        public string ResponseText { get => responseText; set => SetProperty(ref responseText, value); }
        public string ResponseColor { get => responseColor; set => SetProperty(ref responseColor, value); }
        public bool IsRefreshing { get => isRefreshing; set => SetProperty(ref isRefreshing, value); }
        public bool IsVisible { get => isVisible; set => SetProperty(ref isVisible, value); }
        public ObservableCollection<AlbumDTO> Albums { get; set; }

        public AlbumsViewModel()
        {
            LoadPage();
            RefreshCommand = new Command(LoadPage);
            HideCommand = new Command(() =>
            {
                IsVisible = false;
            });
            DeleteCommand = new Command(DeleteAlbum);
        }



        private void DeleteAlbum(object item)
        {
            var album = item as AlbumDTO;

            var client = new RestClient();
            var request = new RestRequest("https://10.0.2.2:5011/api/Albums/" + album.Id);
            request.Method = Method.DELETE;
            var loginResponse = Application.Current.Properties["UserData"] as LoginResponse;
            request.AddHeader("Authorization", "Bearer" + loginResponse.Token);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Albums.Remove(album);
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

        private void LoadPage()
        {
            var client = new RestClient();
            var request = new RestRequest("https://10.0.2.2:5011/api/Albums");
            request.Method = Method.GET;
            var loginResponse = Application.Current.Properties["UserData"] as LoginResponse;
            request.AddHeader("Authorization", "Bearer " + loginResponse.Token);

            var response = client.Execute<IEnumerable<AlbumDTO>>(request);
            Albums = new ObservableCollection<AlbumDTO>(response.Data);
            IsRefreshing = false;
            
        }

        public ICommand RefreshCommand { get; }
        public ICommand HideCommand { get; }
        public ICommand DeleteCommand { get; }


    }
}
