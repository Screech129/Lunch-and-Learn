#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using LnLTest1.Services;

namespace LnLTest1.Data
{
    public class DbHelper
    {
        MobileServiceClient client;
        IMobileServiceSyncTable<ToDoItem> toDoTable;
        private bool initialized;
        public DbHelper ()
        {
            client = new MobileServiceClient("https://lunchandlearntodo.azurewebsites.net");           
        }

        public async Task Initialize()
        {
            var offlineTable = new MobileServiceSQLiteStore("localstore.db");
            offlineTable.DefineTable<ToDoItem>();

            toDoTable = client.GetSyncTable<ToDoItem>();
            await client.SyncContext.InitializeAsync(offlineTable, new SyncService());
            await client.SyncContext.PushAsync();
            initialized = true;
        }

        public async Task Insert (ToDoItem item)
        {
            try
            {
                if (initialized)
                {
                    await Initialize();
                }
                await toDoTable.InsertAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<List<ToDoItem>> Get ()
        {
            if (initialized)
            {
                await Initialize();
            }
            return await toDoTable.ToListAsync();
        }

        public async Task DeleteItem (ToDoItem item)
        {
            if (initialized)
            {
                await Initialize();
            }
            await toDoTable.DeleteAsync(item);
        }

        public async Task Update (ToDoItem item)
        {
            try
            {
                if (initialized)
                {
                    await Initialize();
                }
                item.Done = !item.Done;
               await toDoTable.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
