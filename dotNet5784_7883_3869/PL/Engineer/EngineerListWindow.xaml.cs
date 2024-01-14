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
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp);

        }
        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EnginnerListProperty); }
            set { SetValue(EnginnerListProperty, value); }
        }

        public static readonly DependencyProperty EnginnerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        private void cbExperienceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.None ?
            s_bl?.Engineer.ReadAll() :
            s_bl?.Engineer.ReadAll(item => item.Level == Experience);
            EngineerList = temp == null ? new() : new(temp);
        }

        private void BtnOpenAddOrUpdateWindow_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp);
        }

        private void UpdateThisEngineer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Engineer? enginnerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            if (enginnerInList != null)
                new EngineerWindow(enginnerInList!.ID).ShowDialog(); ;
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp);
          
        }
    
    }
}
