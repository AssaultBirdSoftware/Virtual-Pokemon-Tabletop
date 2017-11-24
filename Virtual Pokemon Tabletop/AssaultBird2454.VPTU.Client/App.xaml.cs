using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AssaultBird2454.VPTU.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Thread thread;
        public App()
        {
            thread = new Thread(new ThreadStart(() =>
            {
                MainWindow window = new MainWindow();
                window.ShowDialog();
                return;
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            Application.Current.Shutdown();
        }
    }
}
