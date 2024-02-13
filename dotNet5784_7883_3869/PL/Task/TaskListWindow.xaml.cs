using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public TaskListWindow()
        {
            InitializeComponent();
            Activated += updateTheListToDisplay!;
            
        }

        public ObservableCollection<BO.Task> TaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));

        public BO.Status status { get; set; } = BO.Status.None;

        private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = status == BO.Status.None ?
            s_bl?.Task.ReadAll()!:
            s_bl?.Task.ReadAll(item => item.TaskStatus == status)!;
            TaskList = temp == null ? new() : new(temp);
        }

        private void UpdateThisTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Task? taskInList = (sender as ListView)?.SelectedItem as BO.Task;
            if (taskInList != null)
                new TaskWindow(taskInList!.ID).ShowDialog(); ;
        }
        private void BtnOpenAddOrUpdateWindow_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
        }
        private void updateTheListToDisplay(Object sender, EventArgs e)
        {
            var temp = s_bl?.Task.ReadAll();
            TaskList = temp == null ? new() : new(temp);
        }

    }
}
