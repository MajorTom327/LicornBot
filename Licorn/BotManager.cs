using Licorn.Keyboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WindowsInput.Native;

namespace Licorn
{
    public class BotManager
    {
        private bool _isStarted = false;
        private KeyboardManager _kb = new KeyboardManager();
        public bool IsStarted { get { return this._isStarted; } }
        public BackgroundWorker Worker { get; set; }

        public BotManager()
        {
            this._kb
                .AddKey(VirtualKeyCode.SPACE);
        }

        public void Execute(object sender, DoWorkEventArgs e)
        {
            this._isStarted = true;
            //Thread.Sleep(500);
            while (this._isStarted)
            {
                if (Worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    this._isStarted = false;
                    return;
                }

                // Process
                this._kb.Execute();
                Thread.Sleep(250);

                Console.WriteLine("Test test");
            }
            this._isStarted = false;
        }
    }
}
