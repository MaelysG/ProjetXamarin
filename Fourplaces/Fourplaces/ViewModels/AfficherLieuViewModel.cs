using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace Fourplaces.ViewModels
{
    class AfficherLieuViewModel : ViewModelBase
    {
        private Position _myPosition;
        public Position MyPosition { get { return _myPosition; } set { _myPosition = value; OnPropertyChanged(); } }
        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection { get { return _pinCollection; } set { _pinCollection = value; OnPropertyChanged(); } }

        public AfficherLieuViewModel(double longitude, double latitude, string name)
        {
            MyPosition = new Position(latitude, longitude);
            PinCollection.Add(new Pin() { Position = MyPosition, Type = PinType.Generic, Label = name });
        }
    }
}
