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
using PL.Engineer;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();
            CurrentEngineer = (Id == 0) ? new BO.Engineer { ID = 0, Name = "",Level = BO.EngineerExperience.None, Cost = 0, Email = "", CurrentTask=null } :
                s_bl.Engineer.Read(Id);
        }


        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        private void BtnAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
