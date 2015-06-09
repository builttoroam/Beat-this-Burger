using Microsoft.Azure.Mobile.Server;

namespace BeatThisBurger.DataObjects
{
    public class Photo : EntityData
    {
        public string BurgerId { get; set; }

        public Burger Burger { get; set; }

        public string ImageUri { get; set; }

        public string SasToken { get; set; }
    }
}