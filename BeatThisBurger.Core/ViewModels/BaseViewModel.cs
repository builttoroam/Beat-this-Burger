using BeatThisBurger.Services;
using Cirrious.MvvmCross.ViewModels;

namespace BeatThisBurger.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        protected IDataService DataService { get; set; }

        public BaseViewModel(IDataService dataService)
        {
            DataService = dataService;
        }
    }
}