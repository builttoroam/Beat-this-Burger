using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Data;
using BeatThisBurger.DataObjects;
using BeatThisBurger.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Views;

namespace BeatThisBurger.Views
{
    public sealed partial class FirstView 
    {
        public FirstView()
        {
            InitializeComponent();
            Data = new DataContextWrapper<FirstViewModel>(this);
        }

        public DataContextWrapper<FirstViewModel> Data { get; private set; }

        private void SortChanged(object sender, RoutedEventArgs e)
        {
            var rdo = sender as RadioButton;
            var txt = rdo.Content +"";
            VisualStateManager.GoToState(this, txt,true);
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SV_Menu.IsPaneOpen = true;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Size.Text = e.NewSize.Width +"";

        }
    }

    public class LocationConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var place = value as Place;
            if (place == null || !place.Latitude.HasValue || !place.Longitude.HasValue) return null;
            return new Geopoint(new BasicGeoposition {Latitude = place.Latitude??0.0, Longitude = place.Longitude??0.0});
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
