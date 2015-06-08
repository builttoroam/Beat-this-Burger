using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatThisBurger
{
    public static partial class Constants
    {
#if DEBUG
        public const string LocalDatabaseFileName = "local_dev.db";
#else
                public const string LocalDatabaseFileName = "local.db";
#endif
    }
}
