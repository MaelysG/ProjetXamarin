using Common.Api.Dtos;
using Fourplaces.Views;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class ConnexionViewModel : ViewModelBase
    {
        public ICommand Connexion { get; }
        public ICommand Inscription { get; }
        private string _errorMessage;
        public string ErrorMessage {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
        public string Mail { get; set; }
        public string Mdp { get; set; }

        public ConnexionViewModel()
        {
            Connexion = new Command(Login);
            Inscription = new Command(SignIn);
            ErrorMessage = "";
        }

        private async void Login(object obj)
        {
            ApiClient client = new ApiClient();
            LoginRequest request = new LoginRequest();
            request.Email = Mail;
            request.Password = Mdp;
            HttpResponseMessage reponse = await client.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/login", request);
            if (reponse.IsSuccessStatusCode)
            {
                await NavigationService.PushAsync(new ListeLieux());
            }
            else
            {
                ErrorMessage = "Utilisateur ou mot de passe invalide";
            }
        }
        private async void SignIn(object obj)
        {
            await NavigationService.PushAsync(new Inscription());
        }

    }
}
