using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using WindowsInput.Native;

namespace Licorn
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BotManager _manager { get; set; }
        public BackgroundWorker worker;

        private List<ActionElement> list_actionActivated = new List<ActionElement>();

        public MainWindow()
        {
            InitializeComponent();
            this._manager = new BotManager();
            this.worker = new BackgroundWorker();
            this.worker.DoWork += this._manager.Execute;
            this.worker.WorkerSupportsCancellation = true;

            List<ActionElement> list_action = new List<ActionElement>() {
                new ActionElement(){ ActionName = "Space", Key = VirtualKeyCode.SPACE },
                new ActionElement(){ ActionName = "Numpad 0", Key = VirtualKeyCode.NUMPAD0 },
                new ActionElement(){ ActionName = "Numpad 1", Key = VirtualKeyCode.NUMPAD1 },
                new ActionElement(){ ActionName = "Numpad 2", Key = VirtualKeyCode.NUMPAD2 },
                new ActionElement(){ ActionName = "Numpad 3", Key = VirtualKeyCode.NUMPAD3 },
                new ActionElement(){ ActionName = "Numpad 4", Key = VirtualKeyCode.NUMPAD4 },
                new ActionElement(){ ActionName = "Numpad 5", Key = VirtualKeyCode.NUMPAD5 },
                new ActionElement(){ ActionName = "Numpad 6", Key = VirtualKeyCode.NUMPAD6 },
                new ActionElement(){ ActionName = "Numpad 7", Key = VirtualKeyCode.NUMPAD7 },
                new ActionElement(){ ActionName = "Numpad 8", Key = VirtualKeyCode.NUMPAD8 },
                new ActionElement(){ ActionName = "Numpad 9", Key = VirtualKeyCode.NUMPAD9 },
            };

            //List<ActionElement> list_actionActivated = new List<ActionElement>();

            ListViewOrigin.ItemsSource = list_action;
            ListViewDest.ItemsSource = list_actionActivated;

            this._manager.Worker = this.worker;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.worker.IsBusy)
            {
                BotStatus.Content = "Is Running...";
                this.worker.RunWorkerAsync();
            }
            e.Handled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.worker.IsBusy)
            {
                BotStatus.Content = "Stopped";
                this.worker.CancelAsync();
            }
            e.Handled = true;
        }

        private void OnSwitchClick(object sender, RoutedEventArgs e)
        {
            //var i = ListViewOrigin.SelectedItem;
            if (ListViewOrigin.SelectedItem != null)
            {
                list_actionActivated.Add((ActionElement)ListViewOrigin.SelectedItem);
                ListViewDest.ItemsSource = null;
                ListViewDest.ItemsSource = list_actionActivated;
            }
            else
            {
                MessageBox.Show("You must select a action before !");
            }

            e.Handled = true;
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            
            if (ListViewDest.SelectedItem != null)
            {
                list_actionActivated.Remove((ActionElement)ListViewDest.SelectedItem);
                ListViewDest.ItemsSource = null;
                ListViewDest.ItemsSource = list_actionActivated;
            }
            else
            {
                MessageBox.Show("You must select a action before !");
            }
            e.Handled = true;
        }
    }
}
