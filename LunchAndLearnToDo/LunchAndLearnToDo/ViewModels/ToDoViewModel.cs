using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LunchAndLearnToDo.Data;
using LunchAndLearnToDo.Models;
using Xamarin.Forms;

namespace LunchAndLearnToDo.ViewModels
{
    public class ToDoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DbHelper dbHelper = new DbHelper();

        private bool done;
        public bool Done
        {
            get
            {
                return done;
            }
            set
            {
                done = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Done"));
            }

        }

        private string newItem;
        public string NewItem
        {
            get
            {
                return newItem;
            }
            set
            {
                newItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NewItem"));

            }

        }

        private ObservableCollection<ToDoItem> toDoItems;
        public ObservableCollection<ToDoItem> ToDoItems
        {
            get
            {
                return toDoItems;
            }
            set
            {
                toDoItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ToDoItems"));
            }
        }

        private ToDoItem selectedItem;

        public ToDoItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        public ICommand AddToDoItem { get; protected set; }

        public ToDoViewModel ()
        {
            AddToDoItem = new Command(async () => await AddItem());
        }


        public async Task FillToDoItems ()
        {
            //await dbHelper.TestData();
            var toDoList = await dbHelper.Get();
            ToDoItems = new ObservableCollection<ToDoItem>(toDoList.Where(x => x.Done == false));
        }

        public async Task AddItem ()
        {
            var item = new ToDoItem
            {
                Done = false,
                Name = NewItem
            };
            await dbHelper.Insert(item);
            await FillToDoItems();
        }

        public async Task CompleteItem ()
        {

            await dbHelper.Update(selectedItem);
            await FillToDoItems();
        }
    }


}
