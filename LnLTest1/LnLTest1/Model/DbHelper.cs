using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;

namespace LnLTest1.Data
{
    public class DbHelper
    {
        MobileServiceClient client;

        public DbHelper ()
        {
            client = new MobileServiceClient("https://lunchandlearntodo.azurewebsites.net");
        }
        public async Task Insert (ToDoItem item)
        {
            try
            {
                await client.GetTable<ToDoItem>().InsertAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public Task<List<ToDoItem>> Get ()
        {
            return client.GetTable<ToDoItem>().ToListAsync();
        }

        public void DeleteDatabase (ToDoItem item)
        {
            client.GetTable<ToDoItem>().DeleteAsync(item);
        }

        public async Task Update (ToDoItem item)
        {
            try
            {
               item.Done = item.Done ? false : true;
               await client.GetTable<ToDoItem>().UpdateAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task TestData ()
        {
            //DeleteDatabase();
            var itemsList = new List<ToDoItem>();
            for (int i = 0; i < 10; i++)
            {
                itemsList.Add(new ToDoItem
                {
                    Done = false,
                    Name = "Test" + i
                });

            }

            foreach (var item in itemsList)
            {
                await Insert(item);
            }

            var test = await Get();
        }
    }
}
