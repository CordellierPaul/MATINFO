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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MATINFO.Metier;

namespace MATINFO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            lvGeneral.Items.Refresh();

            //AccesDonnees accesBD = new AccesDonnees();
            //accesBD.OpenConnection();

            // Test de connexion à la base de données
            //MessageBox.Show("Résultat de la connexion : " + accesBD.OpenConnection());
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }

        private void btCategorieWindow_Click(object sender, RoutedEventArgs e)
        {
            CategorieWindow categorieWindow = new CategorieWindow();
            Close();
            categorieWindow.Show();
        }

        private void btMaterielWindow_Click(object sender, RoutedEventArgs e)
        {
            MaterielWindow materielWindow = new MaterielWindow();
            Close();
            materielWindow.Show();
        }

        private void btPersonnelWindow_Click(object sender, RoutedEventArgs e)
        {
            PersonnelWindow personnelWindow = new PersonnelWindow();
            Close();
            personnelWindow.Show();
        }

        private void btAttribution_Click(object sender, RoutedEventArgs e)
        {
            AttributionWindow attributionWindow = new AttributionWindow();
            Close();
            attributionWindow.Show();
        }
    }
}
