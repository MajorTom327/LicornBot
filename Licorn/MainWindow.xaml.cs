using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;
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
                new ActionElement(){ ActionName = "Primary Weapon", Key = VirtualKeyCode.SPACE },
                new ActionElement(){ ActionName = "Secondary Weapon", Key = VirtualKeyCode.VK_Z },
                new ActionElement(){ ActionName = "Pick Up", Key = VirtualKeyCode.VK_X },
                new ActionElement(){ ActionName = "Q", Key = VirtualKeyCode.VK_Q },
                new ActionElement(){ ActionName = "W", Key = VirtualKeyCode.VK_W },
                new ActionElement(){ ActionName = "E", Key = VirtualKeyCode.VK_E },
                new ActionElement(){ ActionName = "R", Key = VirtualKeyCode.VK_R },
                new ActionElement(){ ActionName = "T", Key = VirtualKeyCode.VK_T },
                new ActionElement(){ ActionName = "Y", Key = VirtualKeyCode.VK_Y },
                new ActionElement(){ ActionName = "C", Key = VirtualKeyCode.VK_C },
                new ActionElement(){ ActionName = "V", Key = VirtualKeyCode.VK_V },
                new ActionElement(){ ActionName = "D", Key = VirtualKeyCode.VK_D },

                new ActionElement(){ ActionName = "1", Key = VirtualKeyCode.VK_1 },
                new ActionElement(){ ActionName = "2", Key = VirtualKeyCode.VK_2 },
                new ActionElement(){ ActionName = "3", Key = VirtualKeyCode.VK_3 },
                new ActionElement(){ ActionName = "4", Key = VirtualKeyCode.VK_4 },
                new ActionElement(){ ActionName = "5", Key = VirtualKeyCode.VK_5 },
                new ActionElement(){ ActionName = "6", Key = VirtualKeyCode.VK_6 },
                new ActionElement(){ ActionName = "7", Key = VirtualKeyCode.VK_7 },
                new ActionElement(){ ActionName = "8", Key = VirtualKeyCode.VK_8 },
                new ActionElement(){ ActionName = "9", Key = VirtualKeyCode.VK_9 },
                new ActionElement(){ ActionName = "0", Key = VirtualKeyCode.VK_0 },

                new ActionElement(){ ActionName = "Numpad 1", Key = VirtualKeyCode.NUMPAD1 },
                new ActionElement(){ ActionName = "Numpad 2", Key = VirtualKeyCode.NUMPAD2 },
                new ActionElement(){ ActionName = "Numpad 3", Key = VirtualKeyCode.NUMPAD3 },
                new ActionElement(){ ActionName = "Numpad 4", Key = VirtualKeyCode.NUMPAD4 },
                new ActionElement(){ ActionName = "Numpad 5", Key = VirtualKeyCode.NUMPAD5 },
                new ActionElement(){ ActionName = "Numpad 6", Key = VirtualKeyCode.NUMPAD6 },
                new ActionElement(){ ActionName = "Numpad 7", Key = VirtualKeyCode.NUMPAD7 },
                new ActionElement(){ ActionName = "Numpad 8", Key = VirtualKeyCode.NUMPAD8 },
                new ActionElement(){ ActionName = "Numpad 9", Key = VirtualKeyCode.NUMPAD9 },
                new ActionElement(){ ActionName = "Numpad 0", Key = VirtualKeyCode.NUMPAD0 },
            };

            ListViewOrigin.ItemsSource = list_action;
            ListViewDest.ItemsSource = list_actionActivated;

            TimerValue.Text = "100";
            Antriopy.Text = "20";

            this._manager.Worker = this.worker;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.worker.IsBusy)
            {
                BotStatus.Content = "Is Running...";
                //this.BotEditorGrid.Visibility = Visibility.Hidden;
                //this.MainWindowView.Height = 255;
                this.AddElementButton.IsEnabled = false;
                this.RemoveButton.IsEnabled = false;
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

            //this.BotEditorGrid.Visibility = Visibility.Visible;

            this.AddElementButton.IsEnabled = true;
            this.RemoveButton.IsEnabled = true;
            e.Handled = true;
        }

        private void OnSwitchClick(object sender, RoutedEventArgs e)
        {
            //var i = ListViewOrigin.SelectedItem;
            if (_manager.IsStarted)
            {
                e.Handled = false;
                return;
            }
            if (ListViewOrigin.SelectedItem != null)
            {
                list_actionActivated.Add((ActionElement)ListViewOrigin.SelectedItem);
                ListViewDest.ItemsSource = null;
                ListViewDest.ItemsSource = list_actionActivated;
                this.RefreshList();
            }
            else
            {
                MessageBox.Show("You must select a action before !");
            }

            e.Handled = true;
        }

        private void RefreshList()
        {
            this._manager.ClearKey();
            foreach (ActionElement e in list_actionActivated)
            {
                this._manager.AddKey(e);
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (_manager.IsStarted)
            {
                e.Handled = false;
                return;
            }

            if (ListViewDest.SelectedItem != null)
            {
                list_actionActivated.Remove((ActionElement)ListViewDest.SelectedItem);
                ListViewDest.ItemsSource = null;
                ListViewDest.ItemsSource = list_actionActivated;
                this.RefreshList();
            }
            else
            {
                MessageBox.Show("You must select a action before !");
            }
            e.Handled = true;
        }

        private void TimerValue_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int val;
            //MessageBox.Show("Value changed !");
            if (int.TryParse(TimerValue.Text, out val)){
                this._manager.TimerSpan = val;
            }
        }

        private void AntriopyChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int val;
            
            if (int.TryParse(TimerValue.Text, out val))
            {
                this._manager.Antriopy = val;
            }
        }

        private void InputNumberFilter(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
