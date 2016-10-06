using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchAndLearnToDo.Models
{
    public class ToDoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        [Version]
        public string Version { get; set; }
    }
}
