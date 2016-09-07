﻿using LnLTest1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace LnLTest1
{
    public class App : Application
    {

        public App ()
        {
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