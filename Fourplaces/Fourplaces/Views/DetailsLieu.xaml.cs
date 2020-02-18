using Fourplaces.ViewModels;
using Xamarin.Forms.Xaml;
using Storm.Mvvm.Forms;

namespace Fourplaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsLieu : BaseContentPage
    {
        public DetailsLieu(int Id)
        {
            BindingContext = new DetailsLieuViewModel(Id);
            InitializeComponent();
        }
    }
}