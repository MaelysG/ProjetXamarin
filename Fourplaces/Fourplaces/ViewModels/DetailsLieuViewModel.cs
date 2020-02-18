using Common.Api.Dtos;
using Storm.Mvvm;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class DetailsLieuViewModel : ViewModelBase
    {
        public ICommand Maps { get; }
        public ICommand Commentaires { get; }
        //public int placeId { get; set; }
        private PlaceItem _placeItem = new PlaceItem();
        public PlaceItem PlaceItem
        {
            get => _placeItem;
            set => SetProperty(ref _placeItem, value);
        }

        public DetailsLieuViewModel(int id)
        {
            GetPlace(id);
            Maps = new Command(Carte);
        }

        private void Carte(object obj)
        {
            throw new NotImplementedException();
        }

        public async void GetPlace(int id)
        {
            ApiClient client = new ApiClient();
            string url = "https://td-api.julienmialon.com/places/" + id;
            HttpResponseMessage reponse = await client.Execute(HttpMethod.Get, url);
            if (reponse.IsSuccessStatusCode)
            {
                Response<PlaceItem> placeReponse = await client.ReadFromResponse<Response<PlaceItem>>(reponse);
                if (placeReponse.IsSuccess)
                {
                    PlaceItem = placeReponse.Data;
                }
            }
        }
    }
}
