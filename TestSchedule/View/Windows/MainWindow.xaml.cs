using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSchedule.Controller;
using TestSchedule.ViewModel;

namespace TestSchedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowController controller;

        private readonly MainWindowViewModel _mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = _mainWindowViewModel = new MainWindowViewModel();



            DayTextBlock.Text = DateTime.Now.ToString("dddd"); // Полное название дня недели

            //вывод текущего времени
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateCurrentTime);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            controller = new MainWindowController();


        }
        private void UpdateCurrentTime(object sender, EventArgs e)
        {
            currentTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}