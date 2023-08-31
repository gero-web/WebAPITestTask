using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CLientTestTask
{
    public class App : Application
    {
        readonly MainWindow window;

        public App(MainWindow window)
        {
            this.window = window;     
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            window.Show();
            base.OnStartup(e);
        }
    }
}
