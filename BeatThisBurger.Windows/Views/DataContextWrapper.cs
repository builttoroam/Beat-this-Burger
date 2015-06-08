using Windows.UI.Xaml;
using Cirrious.MvvmCross.ViewModels;

namespace BeatThisBurger.Views
{
    public class DataContextWrapper<TViewModel> : MvxNotifyPropertyChanged
        where TViewModel:class
    {
        public FrameworkElement Element { get; set; }
        public TViewModel ViewModel => Element?.DataContext as TViewModel;

        public DataContextWrapper(FrameworkElement element)
        {
            if (Element!=null)
            {
                element.DataContextChanged -= DataContextChanged;
            }
            Element = element;
            if (Element!=null)
            {
                element.DataContextChanged += DataContextChanged;
            }
        }

        private void DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            RaisePropertyChanged(()=>ViewModel);
        }
    }
}