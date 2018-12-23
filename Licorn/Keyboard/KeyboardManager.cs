using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace Licorn.Keyboard
{
    
    class KeyboardManager
    {
        public List<KeyAction> _keys = new List<KeyAction>();

        public KeyboardManager()
        {

        }

        public void Execute()
        {
            foreach (KeyAction key in _keys)
            {
                key.Execute();
                Thread.Sleep(250);
            }
        }

        public KeyboardManager AddKey(VirtualKeyCode key)
        {
            _keys.Add(new KeyAction(key));
            return this;
        }
    }
}
