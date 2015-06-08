using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatThisBurger
{
    public static partial class Constants
    {
        public const string GatewayUrl = "https://beat-this-burger62614c1465f646c98f79d20e39610af3.azurewebsites.net";
#if DEBUG
        public const string MobileAppUrl = "http://localhost:34227";
        public const string ApplicationKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
#else
        public const string MobileAppUrl = "https://beatthisburger-code.azurewebsites.net";
        public const string ApplicationKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
#endif
    }
}
