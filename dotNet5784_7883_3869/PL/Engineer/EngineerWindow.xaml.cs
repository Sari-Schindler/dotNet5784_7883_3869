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
using BO;
using DO;
using PL.Engineer;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        private int id = 0;

        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Function to display single engineer for update or create
        /// </summary>
        /// <param name="Id"></param>
        public EngineerWindow(int Id = 0)
        {
            id = Id;
            InitializeComponent();
            CurrentEngineer = (Id == 0) ? new BO.Engineer { ID = 0, Name = "", Level = BO.EngineerExperience.None, Cost = 0, Email = "", CurrentTask = null } :
                s_bl.Engineer.Read(Id);
        }

        /// <summary>
        /// For the experience's enum
        /// </summary>
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        /// <summary>
        /// Function to show the validation's for create or update and show the fit content on the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id == 0) 
                { 
                    s_bl.Engineer.Create(CurrentEngineer);
                    MessageBox.Show("The engineer was added successfully", "added successfully", MessageBoxButton.OK, MessageBoxImage.Information); }
                else
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                    MessageBox.Show("The engineer updated successfully","updates successfully",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.Close();
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message, "an error exception",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DalAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "an error exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlInvalidValueException ex)
            {
                MessageBox.Show(ex.Message, "an error exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "an error exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
