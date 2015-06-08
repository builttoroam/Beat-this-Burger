using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;

namespace BeatThisBurger.DataObjects
{
    public class Burger:EntityData
    {
        public string PlaceId { get; set; }

        public string Name { get; set; }
        public string Ingredients { get; set; }

        public double? Price { get; set; }

        public Place Place { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}