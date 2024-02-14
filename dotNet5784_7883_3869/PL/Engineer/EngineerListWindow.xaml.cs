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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Display the engineer's list
        /// </summary>
        public EngineerListWindow()
        {
            InitializeComponent();
            //var temp = s_bl?.Engineer.ReadAll();
            //EngineerList = temp == null ? new() : new(temp);
            Activated += UpdateTheListToDisplay!;
        }

        public ObservableCollection<BO.EngineerInList> EngineerList
        {
            get { return (ObservableCollection<BO.EngineerInList>)GetValue(EnginnerListProperty); }
            set { SetValue(EnginnerListProperty, value); }
        }

        public static readonly DependencyProperty EnginnerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));

        /// <summary>
        /// Enum's experience
        /// </summary>
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        /// <summary>
        /// Show wanted engineer by specific experience level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbExperienceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.None ?
            s_bl?.EngineerInList.ReadAll().OrderBy(engineer => engineer.Name) :
            s_bl?.EngineerInList.ReadAll(item => item.Level == Experience).OrderBy(engineer => engineer.Name); ;
            EngineerList = temp == null ? new() : new(temp);
        }

        /// <summary>
        /// display update screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenAddOrUpdateWindow_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
        }

        /// <summary>
        /// Select add or update enginner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateThisEngineer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.EngineerInList? enginnerInList = (sender as ListView)?.SelectedItem as BO.EngineerInList;
            if (enginnerInList != null)
                new EngineerWindow(enginnerInList!.ID).ShowDialog(); ;
        }

        /// <summary>
        /// Show updated engineer list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTheListToDisplay(Object sender, EventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.None ?
            s_bl?.EngineerInList.ReadAll().OrderBy(engineer => engineer.Name) :
            s_bl?.EngineerInList.ReadAll(item => item.Level == Experience).OrderBy(engineer => engineer.Name);
            EngineerList = temp == null ? new() : new(temp);
        }
    }
}



