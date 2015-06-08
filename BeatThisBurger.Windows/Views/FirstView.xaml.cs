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
    }
}
