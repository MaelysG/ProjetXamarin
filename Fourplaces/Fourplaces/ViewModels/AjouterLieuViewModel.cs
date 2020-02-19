using System;
using System.Net.Http;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;
using Storm.Mvvm;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Storm.Mvvm.Services;
using Fourplaces.Models;

namespace Fourplaces.ViewModels
{
    class AjouterLieuViewModel : ViewModelBase
    {
        public PlaceItem place { get; set; }
        public ImageItem image { get; set; }
        public ICommand Photo { get; }
        public ICommand Add { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }

        private Position _myPosition;
        public Position MyPosition { get { return _myPosition; } set { _myPosition = value; OnPropertyChanged(); } }

        public AjouterLieuViewModel()
        {
            Photo = new Command(Photograph);
            Add = new Command(Ajouter);
        }

        public override async Task OnResume()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            Lattitude = location.Latitude;
            Longitude = location.Longitude;
        }
        private async void Photograph(object obj)
        {
            ImageSelection selection = await SelectionMode();
            MediaFile file = await PictureService.ChoixImage(selection);
            if (file != null)
            {
                ApiClient client = new ApiClient();
                image = await client.PublishImage(file);
            }
        }

        private async Task<ImageSelection> SelectionMode()
        {
            IDialogService dialog = DependencyService.Get<IDialogService>();
            bool selection = await dialog.DisplayAlertAsync("Source", "D'où voulez vous importer la photo ?", "Caméra", "Gallerie");
            // if true, bind to camera, else Gallery
            if (selection)
            {
                return ImageSelection.FROM_CAMERA;
            }
            else
            {
                return ImageSelection.FROM_GALLERY;
            }
        }

        private async void Ajouter(object obj)
        {
            ApiClient client = new ApiClient();
            CreatePlaceRequest request = new CreatePlaceRequest();
            request.Title = Name;
            request.Description = Description;
            request.Latitude = Lattitude;
            request.Longitude = Longitude;
            if(image != null)
            {
                request.ImageId = image.Id;
            }
            HttpResponseMessage reponse = await client.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/places", request);
            if (reponse.IsSuccessStatusCode)
            {
                await NavigationService.PopAsync();
            }
            else
            {
                IDialogService dialog = DependencyService.Get<IDialogService>();
                await dialog.DisplayAlertAsync("Marche pas", "Marche pas", "Triste");
            }
        }
    }
}
