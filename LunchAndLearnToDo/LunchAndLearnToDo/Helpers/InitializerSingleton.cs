using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LunchAndLearnToDo.Models;
using LunchAndLearnToDo.Services;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace LunchAndLearnToDo.Helpers
{
   public static class InitializerSingleton
   {
       public static MobileServiceClient Client;
        public static async Task Initialize()
        {
            try
            {
                Client = new MobileServiceClient("https://lunchandlearntodo.azurewebsites.net");
                //Setup the offline sync
                var offlineTable = new MobileServiceSQLiteStore("localstore.db");
                offlineTable.DefineTable<ToDoItem>();

                await Client.SyncContext.InitializeAsync(offlineTable);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
 
            
        }
    }
}
