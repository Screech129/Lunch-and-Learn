using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using LunchAndLearnToDo.Models;
using LunchAndLearnToDo.Services;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace LunchAndLearnToDo.Helpers
{
    public class DbHelper
    {
        private static MobileServiceClient Client => InitializerSingleton.Client;
        private bool initialized;
        private static IMobileServiceSyncTable<ToDoItem> ToDoTable => Client.GetSyncTable<ToDoItem>();

        public DbHelper ()
        {
        }

        public async Task Refresh ()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    await ToDoTable.PullAsync(null, null, null, CancellationToken.None);
                }
                else
                {
                    Debug.WriteLine("Currently off line will try next refresh...");

                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
        }

        public async Task Initialize ()
        {
            //Setup the offline sync
            await InitializerSingleton.Initialize();

            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    await ToDoTable.PullAsync(null, null, null, CancellationToken.None);
                }
                else
                {
                    Debug.WriteLine("Currently off line will try next refresh...");
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
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
                await ToDoTable.InsertAsync(item);
                await Refresh();
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
            return await ToDoTable.ToListAsync();
        }

        public async Task DeleteItem (ToDoItem item)
        {
            if (!initialized)
            {
                await Initialize();
            }
            await ToDoTable.DeleteAsync(item);
            await Refresh();
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
                await ToDoTable.UpdateAsync(item);
                await Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
