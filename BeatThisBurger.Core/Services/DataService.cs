﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatThisBurger.DataObjects;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace BeatThisBurger.Services
{
    public class DataService : IDataService
    {
        private MobileServiceClient MobileService { get; } = new MobileServiceClient(
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
            return await All<Burger>();
        }

        public async Task<Rating[]> Ratings()
        {
            return await All<Rating>();
        }

        private async Task Save<TTable>(TTable item)
        {
            var tbl = MobileService.GetSyncTable<TTable>();
            await tbl.InsertAsync(item);
        }


        public async Task SaveBurger(Place place, Burger burger, Rating rating)
        {
            await Save(place);
            await Save(burger);
            await Save(rating);
            await Sync();
        }
    }

    public interface IDataService
    {

        Task<Place[]> Places();

        Task<Burger[]> Burgers();

        Task<Rating[]> Ratings();


        Task SaveBurger(Place place, Burger burger, Rating rating);

    }
}
