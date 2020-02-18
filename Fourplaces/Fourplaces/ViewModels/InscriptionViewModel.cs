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
    class InscriptionViewModel : ViewModelBase
    {
        public ICommand Inscription { get; }
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
        public string Mail { get; set; }
        public string Mdp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public InscriptionViewModel()
        {
            Inscription = new Command(SignIn);
            ErrorMessage = "";
        }

        private async void SignIn(object obj)
        {
            ApiClient client = new ApiClient();
            RegisterRequest request = new RegisterRequest();
            request.Email = Mail;
            request.Password = Mdp;
            request.FirstName = FirstName;
            request.LastName = LastName;
            HttpResponseMessage reponse = await client.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/register", request);
            if (reponse.IsSuccessStatusCode)
            {
                await NavigationService.PushAsync(new ListeLieux());
            }
            else
            {
                ErrorMessage = "Utilisateur ou mot de passe invalide";
            }
        }
    }
}
