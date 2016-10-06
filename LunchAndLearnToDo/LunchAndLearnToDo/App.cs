using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LunchAndLearnToDo.Views;
using Xamarin.Forms;

namespace LunchAndLearnToDo
{
    public class App : Application
    {
        public App ()
        {
            // The root page of your application
            var toDoPage = new ToDoView();
            MainPage = new NavigationPage(toDoPage);
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
