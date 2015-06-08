using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.AppService;
using BeatThisBurger.DataObjects;
using BeatThisBurger.Service.Models;

namespace BeatThisBurger.Service.Controllers
{
    public class PlaceController : BaseTableController<Place> {}
    public class BurgerController : BaseTableController<Burger> { }
    public class RatingController : BaseTableController<Rating> { }
}