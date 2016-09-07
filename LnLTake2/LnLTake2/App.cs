using AppServiceHelpers.Abstractions;
using LnLTake2.Helpers;
using LnLTake2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LnLTake2
{
    public class App : Application
    {
        public App ()
        {
            AppServiceHelper helper = new AppServiceHelper();
            

            var mainView = new ToDoView(helper.Init());
            MainPage = new NavigationPage(mainView);
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
