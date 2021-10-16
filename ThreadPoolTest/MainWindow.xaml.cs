using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ThreadPoolTest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isClosing = false;
        Task task = null;

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += async (s, e) =>
            {
                if (isClosing)
                {
                    e.Cancel = true;
                    Console.WriteLine("Window Closing Cancel");
                }
                else
                {
                    if (task != null && !task.IsCompleted)
                    {
                        isClosing = true;
                        e.Cancel = true;
                        await task;
                        isClosing = false;
                        Application.Current.Shutdown();
                    }
                    Console.WriteLine("Window Closing");
                }
            };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("START : Button_Click");
            task = Task.Run(async () =>
            {
                await Delay();
            });
            await task;
            Console.WriteLine("FINISH: Button_Click");
        }

        private async Task Delay()
        {
            Console.WriteLine("START : Task.Delay");
            await Task.Delay(5000);
            Console.WriteLine("FINISH: Task.Delay");
        }
    }
}
