using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;

namespace BeatThisBurger.DataObjects
{
    public class Place : EntityData
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

#if !SERVER
        public void RefreshLocation()
        {
            RaisePropertyChanged(() => Latitude);
            RaisePropertyChanged(()=>Longitude);
        }
#endif

        public virtual ICollection<Burger> Burgers { get; set; }
    }
}