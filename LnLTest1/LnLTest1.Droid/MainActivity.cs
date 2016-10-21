using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Realms;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using LnLTest1.Authorization;
using LnLTest1.Data;
using LnLTest1.ViewModels;

namespace LnLTest1.Droid
{
    [Activity(Label = "LnLTest1", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,IAuthenticate
    {
        private MobileServiceUser user;
        protected override void OnCreate (Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CurrentPlatform.Init();
            //App.Init((IAuthenticate)this);
            LoadApplication(new App());
        }

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                var helper = new DbHelper();
                // Sign in with Facebook login using a server-managed flow.
                user = await helper.client.LoginAsync(this,
                    MobileServiceAuthenticationProvider.MicrosoftAccount);
                if (user != null)
                {
                    message = string.Format("you are now signed-in as {0}.",
                        user.UserId);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }
    }
}

