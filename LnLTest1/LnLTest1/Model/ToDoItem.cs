using Microsoft.WindowsAzure.MobileServices;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LnLTest1.Data
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
