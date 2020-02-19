using Common.Api.Dtos;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;
using Fourplaces.Views;

namespace Fourplaces.ViewModels
{
    class ListLieuxViewModel : ViewModelBase
    {
        public ObservableCollection<PlaceItemSummary> Places { get; set; }
        public ICommand Add { get; }

        public ListLieuxViewModel()
        {
            Places = new ObservableCollection<PlaceItemSummary>();
            Add = new Command(Ajouter);
        }

        public override async Task OnResume()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            List<PlaceItemSummary> Lieux = await GetAllPlaces();
            PlaceItemSummary l = new PlaceItemSummary();
            foreach (var lieu in Lieux)
            {
                Places.Add(lieu);
                l = lieu;
            }
            // Places.OrderBy(i => i.Latitude);
        }

        public async Task<List<PlaceItemSummary>> GetAllPlaces()
        {
            ApiClient client = new ApiClient();
            HttpResponseMessage reponse = await client.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/places");
            if (reponse.IsSuccessStatusCode)
            {
                Response<List<PlaceItemSummary>> placesReponse = await client.ReadFromResponse<Response<List<PlaceItemSummary>>>(reponse);
                if (placesReponse.IsSuccess)
                {
                    return placesReponse.Data;
                }
            }
            return null;
        }

        public async void ShowDetails(PlaceItemSummary place)
        {
            await NavigationService.PushAsync(new DetailsLieu(place.Id));
        }

        private async void Ajouter(object obj)
        {
            await NavigationService.PushAsync(new AjouterLieu());
        }
    }
}
