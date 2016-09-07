using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServiceHelpers;
using LnLTake2.Models;
using AppServiceHelpers.Abstractions;

namespace LnLTake2.Helpers
{
    public class AppServiceHelper
    {
        public AppServiceHelper ()
        {

        }

        public IEasyMobileServiceClient Init ()
        {
            var client = EasyMobileServiceClient.Create();
            client.Initialize("http://lunchandlearntodo.azurewebsites.net");
            client.RegisterTable<ToDoItem>();
            client.FinalizeSchema();
            return client;
        }

    }
}
