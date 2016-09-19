using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using LnLTest1.Views;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;

namespace LnLTest1.Services
{
    public class SyncService : IMobileServiceSyncHandler
    {
        public SyncService ()
        {
            
        }
        public async Task<JObject> ExecuteTableOperationAsync (IMobileServiceTableOperation operation)
        {
            JObject result = null;
            MobileServicePreconditionFailedException conflictError = null;
            Debug.WriteLine("Beginning Sync");
            do
            {
                try
                {
                    result = await operation.ExecuteAsync();
                }
                catch (MobileServicePreconditionFailedException e)
                {
                    conflictError = e;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }

                if (conflictError != null)
                {

                    JObject serverItem = conflictError.Value;

                    if (serverItem == null)
                    {
                        serverItem = (JObject)(await operation.Table.LookupAsync((string)operation.Item[MobileServiceSystemColumns.Id]));
                    }


                    operation.Item[MobileServiceSystemColumns.Version] = serverItem[MobileServiceSystemColumns.Version];
                }
            } while (conflictError != null);

            return result;
        }

        public Task OnPushCompleteAsync (MobileServicePushCompletionResult result)
        {
            foreach (var error in result.Errors)
            {
                Debug.WriteLine(error);
            }

            return Task.FromResult(0);
        }
    }
}
