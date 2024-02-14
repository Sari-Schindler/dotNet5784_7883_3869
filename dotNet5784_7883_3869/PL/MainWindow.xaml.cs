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
        /// <summary>
        /// Function to display the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function for display engineer's details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        /// <summary>
        /// Function to displat the task's details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }

        /// <summary>
        /// Function to intalize database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToInitializeDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to initialize Database?", "Initialize Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                DalTest.Initialization.Do(Factory.Get);
            
        }
    }
}
