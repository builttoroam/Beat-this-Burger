﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatThisBurger.DataObjects;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;

namespace BeatThisBurger.Services
{
    public class DataService : IDataService
    {
        private MobileServiceClient MobileService
        { get; }
        = new MobileServiceClient(
            Constants.MobileAppUrl,
            Constants.GatewayUrl,
            Constants.ApplicationKey);
        private MobileServiceSQLiteStore Store { get; } = new MobileServiceSQLiteStore(Constants.LocalDatabaseFileName);

        private bool HasInitialized { get; set; }

        private async Task Initialize()
        {
            if (HasInitialized) return;
            Store.DefineTable<Place>();
            Store.DefineTable<Burger>();
            Store.DefineTable<Rating>();
            await MobileService.SyncContext.InitializeAsync(Store, new MobileServiceSyncHandler());
            HasInitialized = true;
        }

        public const string ADTenant = "beatthisburger.onmicrosoft.com";
        public const string ADAuthority = "https://login.windows.net/" + ADTenant;

        public const string ADNativeClientApplicationClientId = "ef447528-52c2-4c27-a18a-36baadb026c8";

        public const string ADRedirectUri =
            "https://beat-this-burger62614c1465f646c98f79d20e39610af3.azurewebsites.net/login/done";

        public const string MobileServiceAppIdUri =
            "https://beat-this-burger62614c1465f646c98f79d20e39610af3.azurewebsites.net/login/aad";

        public async Task Authenticate(IPlatformParameters parameters)
        {
            try
            {
                var authContext = new AuthenticationContext(ADAuthority);


                // TODO: Remove this - only here to force auth dialog for demo
                //authContext.TokenCache.Clear();

                var authResult = await authContext.AcquireTokenAsync(
                    MobileServiceAppIdUri,
                    ADNativeClientApplicationClientId,
                    new Uri(ADRedirectUri), parameters);
                Debug.WriteLine(authResult != null);

                var jobj = new JObject {["access_token"] = authResult.AccessToken };
                await MobileService.LoginAsync(MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, jobj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }



        public async Task Sync()
        {
            await Initialize();

            await MobileService.SyncContext.PushAsync();

            await Sync<Place>();
            await Sync<Burger>();
            await Sync<Rating>();
        }

        private async Task Sync<TTable>()
        {
            var tbl = MobileService.GetSyncTable<TTable>();
            await tbl.PullAsync(typeof(TTable).Name, tbl.Where(x => false));
        }


        private async Task<TTable[]> All<TTable>()
        {
            await Initialize();
            var tbl = MobileService.GetTable<TTable>();
            return (await tbl.ToListAsync()).ToArray();
        }

        public async Task<Place[]> Places()
        {
            return await All<Place>();
        }

        public async Task<Burger[]> Burgers()
        {
            var ratings = await Ratings();
            var photos = await Photos();
            var burgers = await All<Burger>();

            foreach (var burger in burgers)
            {
                burger.Ratings = ratings.Where(r => r.BurgerId == burger.Id).ToArray();
                burger.Photos = photos.Where(r => r.BurgerId == burger.Id).ToArray();
            }
            return burgers;
        }

        public async Task<Rating[]> Ratings()
        {
            return await All<Rating>();
        }

        public async Task<Photo[]> Photos()
        {
            return await All<Photo>();
        }

        private async Task Save<TTable>(TTable item)
        {
            var tbl = MobileService.GetSyncTable<TTable>();
            await tbl.InsertAsync(item);
        }


        public async Task SaveBurger(Place place, Burger burger, Rating rating, params Photo[] photos)
        {
            await Save(place);
            await Save(burger);
            await Save(rating);
            foreach (var photo in photos)
            {
                await Save(photo);
            }
            await Sync();
        }
    }

    public interface IDataService
    {

        Task<Place[]> Places();

        Task<Burger[]> Burgers();

        Task<Rating[]> Ratings();

        Task<Photo[]> Photos();

        Task SaveBurger(Place place, Burger burger, Rating rating, params Photo[] photos);

        Task Authenticate(IPlatformParameters parameters);
    }
}
