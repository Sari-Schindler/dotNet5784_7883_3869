using DalApi;
using PL.Engineer;
using PL.Task;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }
        private void btnToInitializeDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to initialize Database?", "Initialize Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                DalTest.Initialization.Do(Factory.Get);
            
        }
    }
}
