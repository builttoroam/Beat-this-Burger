using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatThisBurger.DataObjects;
using BeatThisBurger.Services;
using Cirrious.MvvmCross.ViewModels;

namespace BeatThisBurger.ViewModels
{
    public class NewBurgerViewModel:BaseViewModel
    {
        public Place Place { get; } = new Place();
        public Burger Burger { get; } = new Burger();
        public Rating Rating { get; } = new Rating();

        private async Task Save()
        {
            await DataService.SaveBurger(Place, Burger, Rating);

            Close(this);
        }

        public NewBurgerViewModel(IDataService dataService) : base(dataService)
        {
        }

        public async void SaveBurger()
        {
            await Save();

        }

        public void CancelBurger()
        {
            Close(this);
        }

    }
}
