using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using LunchAndLearnToDo.Models;
using LunchAndLearnToDo.Services;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace LunchAndLearnToDo.Data
{
    public class DbHelper
    {
        private MobileServiceClient client;
        private bool initialized;
        IMobileServiceSyncTable<ToDoItem> toDoTable;

        public DbHelper ()
        {
            client = new MobileServiceClient("https://test123456755.azurewebsites.net");
        }

        public async Task Initialize ()
        {
            //Setup the offline sync
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
                if (!initialized)
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
            if (!initialized)
            {
                await Initialize();
            }
            return await toDoTable.ToListAsync();
        }

        public async Task DeleteItem (ToDoItem item)
        {
            if (!initialized)
            {
                await Initialize();
            }
            await toDoTable.DeleteAsync(item);
        }

        public async Task Update (ToDoItem item)
        {
            try
            {
                if (!initialized)
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
