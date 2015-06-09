using System.Reflection;
using Microsoft.Azure.Mobile.Server;

namespace BeatThisBurger.DataObjects
{
    public class Rating : EntityData
    {
        public string BurgerId { get; set; }


        public double Rate { get; set; }

        public Burger Burger{ get; set; }
    }

}