using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ActiveWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public MainWindow()
        {
            InitializeComponent();

            var timer = new System.Timers.Timer()
            {
                Interval = 1000,
                AutoReset = true,
                Enabled = true
            };

            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            string title = GetActiveWindowTitle();
            Dispatcher.BeginInvoke(() => txtCurrentWindow.Text = title);
        }


        private string GetActiveWindowTitle()
        {
            IntPtr windowPtr = GetForegroundWindow();
            int titleLength = GetWindowTextLength(windowPtr) + 1;
            StringBuilder strBuilder = new StringBuilder(titleLength);

            if (GetWindowText(windowPtr, strBuilder, titleLength) > 0)
            {
                return strBuilder.ToString();
            }
            return "";
        }

    }
}
