using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Forms;
using LnLTake2.Models;
using Xamarin.Forms;
using System.Windows.Input;

namespace LnLTake2.ViewModels
{
    public class ToDoItemsViewModel : BaseAzureViewModel<ToDoItem>
    {
        IEasyMobileServiceClient client;
        public ToDoItemsViewModel (IEasyMobileServiceClient client) : base (client)
        {
            this.client = client;

            Title = "To Do List";
        }

        Models.ToDoItem selectedToDoItem;
        public Models.ToDoItem SelectedToDoItem
        {
            get { return selectedToDoItem; }
            set
            {
                selectedToDoItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private ICommand _addNewItemCommand;
        public ICommand AddNewItemCommand
        {
            get
            {
                _addNewItemCommand = _addNewItemCommand ?? new Command(() =>
                {
                    var navigation = Application.Current.MainPage as NavigationPage;
                });
                return _addNewItemCommand;
            }
        }

    }
}
}
