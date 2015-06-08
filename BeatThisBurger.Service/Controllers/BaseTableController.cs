using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using BeatThisBurger.Service.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;

namespace BeatThisBurger.Service.Controllers
{
    public class BaseTableController<TEntity> : TableController<TEntity>
        where TEntity : class, ITableData
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TEntity>(context, Request, Services);
        }

        // GET tables/TEntity
        public IQueryable<TEntity> GetAll()
        {
            return Query();
        }

        // GET tables/TEntity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TEntity> Get(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TEntity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TEntity> Patch(string id, Delta<TEntity> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TEntity
        public async Task<IHttpActionResult> Post(TEntity item)
        {
            TEntity current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TEntity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task Delete(string id)
        {
            return DeleteAsync(id);
        }
    }
}