using System.Collections.Generic;
#if SERVER
using System.ComponentModel.DataAnnotations.Schema;
#endif
using System.Linq;
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

        public virtual ICollection<Photo> Photos { get; set; }

#if SERVER
        [NotMapped]
#endif
        public string FirstPhoto => Photos?.FirstOrDefault()?.ImageUri??"ms-appx:///Assets/BurgerIcon.png";
    }
}