using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    }
}
