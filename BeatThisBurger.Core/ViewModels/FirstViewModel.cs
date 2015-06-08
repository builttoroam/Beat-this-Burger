using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;

namespace BeatThisBurger.ViewModels
{
    public class FirstViewModel:MvxViewModel
    {
        private string hello;

        public string Hello
        {
            get { return hello; }
            set
            {
                hello = value;
                RaisePropertyChanged();
            }
        }

        public void RecordNewBurger()
        {
            ShowViewModel<NewBurgerViewModel>();
        }

    }
}
