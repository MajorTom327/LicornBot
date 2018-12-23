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

        public int TimerSpan { get; set; }
        public int Antriopy { get; set; }

        public KeyboardManager()
        {

        }

        public void Execute()
        {
            System.Random rand = new System.Random();
            
            foreach (KeyAction key in _keys)
            {
                key.Execute();
                
                Thread.Sleep(TimerSpan + 
                    rand.Next(this.Antriopy) - this.Antriopy / 2);
            }
        }

        public KeyboardManager AddKey(VirtualKeyCode key)
        {
            _keys.Add(new KeyAction(key));
            return this;
        }

        public KeyboardManager Clear()
        {
            this._keys.Clear();
            return (this);
        }
    }
}
