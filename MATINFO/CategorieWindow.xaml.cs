using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Logique d'interaction pour CategorieRep.xaml
    /// </summary>
    public partial class CategorieWindow : Window
    {
        public CategorieWindow()
        {
            InitializeComponent();

            lvCategorie.ItemsSource = donneesActuelles.LesCategories;

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
            if (lvCategorie.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette catégorie ?", "Confirmation de suppression", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    //LesCategories.Remove((CategorieMateriel)lvCategorie.SelectedItem);

                    lvCategorie.SelectedIndex = 0;
                }
            }
        }
    }
}
