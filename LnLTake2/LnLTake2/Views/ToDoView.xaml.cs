using AppServiceHelpers.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LnLTake2.Views
{
    public partial class ToDoView : ContentPage
    {
        public ToDoView (IEasyMobileServiceClient client)
        {
            InitializeComponent();
            BindingContext = new ViewModels.ToDoItemsViewModel(client);
        }
    }
}
