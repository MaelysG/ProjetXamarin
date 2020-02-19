using Fourplaces.ViewModels;
using Xamarin.Forms.Xaml;
using Storm.Mvvm.Forms;

namespace Fourplaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AfficherLieuMap : BaseContentPage
    {
        public AfficherLieuMap(double longitude, double latitude, string name)
        {
            BindingContext = new AfficherLieuViewModel(longitude, latitude, name);
            InitializeComponent();
        }
    }
}
