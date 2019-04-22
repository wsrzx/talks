using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AzureTechNights.Models;
using AzureTechNights.Services;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace AzureTechNights.Services
{
    public class AzureService
    {
        private readonly string azureMobileAppUrl = "https://event-lineup.azurewebsites.net";

        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<Session> sessionTable;

        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            Client = new MobileServiceClient(azureMobileAppUrl);

            var path = "session_store.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<Session>();

            await Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            sessionTable = Client.GetSyncTable<Session>();
        }

        public async Task SyncSessions()
        {
            try
            {
                await Client.SyncContext.PushAsync();
                await sessionTable.PullAsync("allSessions", sessionTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Opa, deu merda: " + ex);
            }

        }

        public async Task<IEnumerable<Session>> GetSessions()
        {
            await Initialize();

            if (CrossConnectivity.Current.IsConnected)
            {
                await SyncSessions();
            }

            return await sessionTable.OrderBy(s => s.Day).ThenBy(s => s.StartAt).ToEnumerableAsync();
        }

    }
}
