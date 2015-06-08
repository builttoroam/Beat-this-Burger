using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BeatThisBurger.DataObjects;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BeatThisBurger.Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private MobileServiceClient MobileService { get; } = new MobileServiceClient(Constants.MobileAppUrl, Constants.GatewayUrl, Constants.ApplicationKey);

        private MobileServiceSQLiteStore Store { get; } = new MobileServiceSQLiteStore(Constants.LocalDatabaseFileName);

        private bool HasInitialized { get; set; }
        private async Task Initialize()
        {
            if (HasInitialized) return;
            Store.DefineTable<Burger>();
            Store.DefineTable<Place>();
            Store.DefineTable<Rating>();
            await MobileService.SyncContext.InitializeAsync(Store, new MobileServiceSyncHandler());
            HasInitialized = true;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

    



            await Initialize();

            await MobileService.SyncContext.PushAsync();
            var tbl = MobileService.GetSyncTable<Burger>();
            await tbl.PullAsync("items", tbl.Where(x => true));


            tbl = MobileService.GetSyncTable<Burger>();
            var items = (await tbl.ToListAsync()).ToArray();
            Debug.WriteLine(items.Length);
        }


    }
}
