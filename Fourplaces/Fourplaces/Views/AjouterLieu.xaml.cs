﻿using Fourplaces.ViewModels;
using Xamarin.Forms.Xaml;
using Storm.Mvvm.Forms;

namespace Fourplaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjouterLieu : BaseContentPage
    {
        public AjouterLieu()
        {
            BindingContext = new AjouterLieuViewModel();
            InitializeComponent();
        }
    }
}