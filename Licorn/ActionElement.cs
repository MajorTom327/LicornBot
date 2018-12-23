using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace Licorn
{
    class ActionElement
    {
        public string ActionName { get; set; }
        public VirtualKeyCode Key { get; set; }

        public ActionElement(string name, VirtualKeyCode key)
        {
            this.ActionName = name;
            this.Key = key;
        }

        public ActionElement()
        {

        }
    }
}
