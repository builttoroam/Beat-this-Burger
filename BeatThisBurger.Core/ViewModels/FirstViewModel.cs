using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatThisBurger.DataObjects;
using BeatThisBurger.Services;
using Cirrious.MvvmCross.ViewModels;
using Microsoft.WindowsAzure.MobileServices;

namespace BeatThisBurger.ViewModels
{
    public class FirstViewModel:BaseViewModel
    {

        public FirstViewModel(IDataService dataService):base(dataService)
        {
            
        }
     
        public ObservableCollection<Place> Places { get; }=new ObservableCollection<Place>();

        public ObservableCollection<Burger> Burgers { get; } = new ObservableCollection<Burger>();



        public async override void Start()
        {
            base.Start();

            var places = await DataService.Places();
            foreach (var place in places)
            {
                Places.Add(place);
            }

            var burgers = await DataService.Burgers();
            foreach (var b in burgers)
            {
                Burgers.Add(b);
            }

        }


        public void RecordNewBurger()
        {
            ShowViewModel<NewBurgerViewModel>();
        }

    }
}
