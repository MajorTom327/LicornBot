using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Licorn.Keyboard
{
    class KeyAction
    {
        VirtualKeyCode _key;

        public KeyAction(VirtualKeyCode key)
        {
            this._key = key;
        }

        public void Execute()
        {
            InputSimulator inputSim = new InputSimulator();
            inputSim.Keyboard.KeyPress(this._key);
        }
    }
}
