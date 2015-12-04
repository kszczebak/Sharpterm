using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Windows.Forms;

namespace ArTerm
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            MainWindow mainWindow = new MainWindow(900, 600);
        }

       
    }
}
