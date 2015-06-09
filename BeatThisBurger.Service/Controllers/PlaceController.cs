using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.AppService;
using BeatThisBurger.DataObjects;
using BeatThisBurger.Service.Models;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BeatThisBurger.Service.Controllers
{
    public class PlaceController : BaseTableController<Place> {}
    public class BurgerController : BaseTableController<Burger> { }
    public class RatingController : BaseTableController<Rating> { }

    public class PhotoController : BaseTableController<Photo>
    {
        public async override Task<IHttpActionResult> Post(Photo item)
        {
            string storageAccountName;
            string storageAccountKey;

            // Try to get the Azure storage account token from app settings.  
            if (!(Services.Settings.TryGetValue("STORAGE_ACCOUNT_NAME", out storageAccountName) |
            Services.Settings.TryGetValue("STORAGE_ACCOUNT_ACCESS_KEY", out storageAccountKey)))
            {
                Services.Log.Error("Could not retrieve storage account settings.");
            }

            // Set the URI for the Blob Storage service.
            var blobEndpoint = new Uri($"https://{storageAccountName}.blob.core.windows.net");

            // Create the BLOB service client.
            var blobClient = new CloudBlobClient(blobEndpoint,
                new StorageCredentials(storageAccountName, storageAccountKey));

            var containerName = Constants.PhotosContainerName;

                // Set the BLOB store container name on the item, which must be lowercase.
                containerName = containerName.ToLower();

                // Create a container, if it doesn't already exist.
                var container = blobClient.GetContainerReference(containerName);
                await container.CreateIfNotExistsAsync();

                // Create a shared access permission policy. 
            var containerPermissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            // Enable anonymous read access to BLOBs.
            container.SetPermissions(containerPermissions);

                // Define a policy that gives write access to the container for 5 minutes.                                   
                var sasPolicy = new SharedAccessBlobPolicy()
                {
                    SharedAccessStartTime = DateTime.UtcNow,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5),
                    Permissions = SharedAccessBlobPermissions.Write
                };

                // Get the SAS as a string.
                item.SasToken = container.GetSharedAccessSignature(sasPolicy);

                // Set the URL used to store the image.
                item.ImageUri = $"{blobEndpoint}{containerName}/{Guid.NewGuid().ToString().ToLower()}.jpg";

            return await base.Post(item);
        }
    }
}