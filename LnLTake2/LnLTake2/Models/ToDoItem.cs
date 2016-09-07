using AppServiceHelpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LnLTake2.Models
{
    public class ToDoItem:EntityData
    {
        public string Name { get; set; }
        public bool Done { get; set; }

    }
}
