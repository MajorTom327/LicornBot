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

        public int TimerSpan { get; set; }
        public int Antriopy { get; set; }
        public bool IsStarted { get { return this._isStarted; } }
        public BackgroundWorker Worker { get; set; }

        public BotManager()
        {
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
                this._kb.TimerSpan = this.TimerSpan;
                this._kb.Antriopy = this.Antriopy;
                this._kb.Execute();
            }
            this._isStarted = false;
        }

        public BotManager ClearKey()
        {
            this._kb.Clear();
            return (this);
        }

        public BotManager AddKey(ActionElement e)
        {
            this._kb.AddKey(e.Key);
            return (this);
        }
    }
}
