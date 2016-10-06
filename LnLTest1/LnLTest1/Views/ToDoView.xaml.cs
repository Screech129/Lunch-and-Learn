using LnLTest1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LnLTest1.Views
{
    public partial class ToDoView : ContentPage
    {
        public ToDoView ()
        {
            InitializeComponent();

            ToDoList.ItemSelected += async (s, e) => await CompleteItem();
        }

        protected async override void OnAppearing ()
        {
            base.OnAppearing();
            var vm = BindingContext as ToDoListViewModel;
            if (vm.Authenticated)
            {
                await vm.FillToDoItems();
                this.loginButton.IsVisible = false;
            }
        }



        private async Task CompleteItem ()
        {
            var vm = BindingContext as ToDoListViewModel;
            await vm.CompleteItem();
        }
    }
}
