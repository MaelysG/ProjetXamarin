using Plugin.Media;
using Plugin.Permissions;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Storm.Mvvm.Services;
using Xamarin.Forms;

namespace Fourplaces.Models
{    public enum ImageSelection
    {
        FROM_GALLERY,
        FROM_CAMERA,
    }

    class PictureService
    {
        public static async Task<MediaFile> ChoixImage(ImageSelection pictureService)
        {
            await CrossMedia.Current.Initialize();

            if (pictureService == ImageSelection.FROM_GALLERY)
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    IDialogService dialog = DependencyService.Get<IDialogService>();
                    await dialog.DisplayAlertAsync("Error", "No media available", "Close");
                    return null;
                }

                return await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions(){ PhotoSize = PhotoSize.Small,});
            }

            if (pictureService == ImageSelection.FROM_CAMERA)
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    IDialogService dialog = DependencyService.Get<IDialogService>();
                    await dialog.DisplayAlertAsync("Error", "No media available", "Close");
                    return null;
                }

                return await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Small,
                });
            }

            return null;
        }
    }
}