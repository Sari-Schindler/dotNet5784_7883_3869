using BO;
using DO;
using PL.Engineer;
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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private int id = 0;

        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public TaskWindow(int Id = 0)
        {
            id = Id;
            InitializeComponent();
            CurrentTask = (Id == 0) ? new BO.Task
            {
                Description = "",
                Milestone = null,
                requierdTime = TimeSpan.MinValue,
                CreatedDateTask = DateTime.MinValue,
                EstimatedStartTime = DateTime.MinValue,
                StartTime = DateTime.MinValue,
                TaskStatus = BO.Status.Scheduled,
                DependencysList = null,
                TimeEstimatedLeft = DateTime.MinValue,
                DeadLine = DateTime.MinValue,
                CompleteDate = DateTime.MinValue,
                productDescription = "",
                ComplexityLevel = BO.TaskLevel.None,
                nickName = "",
                Comments = "",
                ID = 0,
                CurrentEngineer = null,
            } :
                s_bl.Task.Read(Id);
        }


        public BO.TaskLevel Experience { get; set; } = BO.TaskLevel.None;

        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
        private void BtnAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id == 0)
                {
                    s_bl.Task.Create(CurrentTask);
                    MessageBox.Show("The task was added successfully", "added successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    s_bl.Task.Update(CurrentTask);
                    MessageBox.Show("The task updated successfully", "updates successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.Close();
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message, "an error exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DalAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "an error exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlInvalidValueException ex)
            {
                MessageBox.Show(ex.Message, "an error exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message, "an error exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
