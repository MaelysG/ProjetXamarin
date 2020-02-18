using Fourplaces.ViewModels;
using Xamarin.Forms.Xaml;
using Storm.Mvvm.Forms;
using TD.Api.Dtos;
using System;

namespace Fourplaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListeLieux : BaseContentPage
    { 
        public ListeLieux()
        {
            BindingContext = new ListLieuxViewModel();
            InitializeComponent();
        }

        private void ShowDetails(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            PlaceItemSummary p = e.Item as PlaceItemSummary;
            Console.WriteLine(p.Id);
            ((ListLieuxViewModel)BindingContext).ShowDetails(e.Item as PlaceItemSummary);
        }
    }
}
