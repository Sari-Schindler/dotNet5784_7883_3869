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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        public EngineerWindow(int Id=0)
        {
            InitializeComponent();
            (Id == 0) ? CurrentEngineer = new ObservableCollection<BO.Engineer>( 0, "", "", 0) :
                s_bl.Engineer.Read(Id);
            ;
        }


        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;

        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public ObservableCollection<BO.Engineer> CurrentEngineer
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EnginnerProperty); }
            set { SetValue(EnginnerProperty, value); }
        }

        public static readonly DependencyProperty EnginnerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

    }
}
