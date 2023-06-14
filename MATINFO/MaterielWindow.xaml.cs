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
using MATINFO.Metier;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour MaterielRep.xaml
    /// </summary>
    public partial class MaterielWindow : Window
    {
        public MaterielWindow()
        {
            InitializeComponent();

            DataContext = this;
        }
        
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce matériel ?", "Confirmation de suppression", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    //LesMateriels.Remove((Materiel)lvMateriel.SelectedItem);

                    lvMateriel.SelectedIndex = 0;
                }
            }
        }
    }
}
