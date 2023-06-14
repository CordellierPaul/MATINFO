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

            AccesDonnees accesBD = new AccesDonnees();
            accesBD.OpenConnection();

            // Test de connexion à la base de données
            //MessageBox.Show("Résultat de la connexion : " + accesBD.OpenConnection());
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Close();
            window.Show();
        }

        private void btCategorieWindow_Click(object sender, RoutedEventArgs e)
        {
            CategorieWindow categorieRep = new CategorieWindow();
            Close();
            categorieRep.Show();
        }

        private void btMaterielWindow_Click(object sender, RoutedEventArgs e)
        {
            MaterielWindow materielRep = new MaterielWindow();
            Close();
            materielRep.Show();
        }

        private void btPersonnelWindow_Click(object sender, RoutedEventArgs e)
        {
            PersonnelWindow personnelRep = new PersonnelWindow();
            Close();
            personnelRep.Show();
        }
    }
}
