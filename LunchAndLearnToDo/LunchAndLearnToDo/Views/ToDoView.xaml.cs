using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunchAndLearnToDo.Helpers;
using LunchAndLearnToDo.Helpers;
using LunchAndLearnToDo.ViewModels;
using Xamarin.Forms;

namespace LunchAndLearnToDo.Views
{
    public partial class ToDoView : ContentPage
    {
        public ToDoView ()
        {
            InitializeComponent();

            ToDoList.ItemSelected += async (s, e) => await CompleteItem();
        }

        protected override async void OnAppearing ()
        {
            base.OnAppearing();
            var vm = BindingContext as ToDoViewModel;
            await vm.FillToDoItems();
        }





        private async Task CompleteItem ()
        {
            var vm = BindingContext as ToDoViewModel;
            await vm.CompleteItem();
        }

        private async void ToDoList_OnRefreshing (object sender, EventArgs e)
        {
            var db = new DbHelper();
            await db.Refresh();
            ToDoList.EndRefresh();
        }
    }
}
