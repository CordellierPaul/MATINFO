using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MATINFO.Metier;
using Microsoft.Win32;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour CategorieRep.xaml
    /// </summary>
    public partial class CategorieWindow : Window
    {
        #region Constructeur
        public CategorieWindow()
        {
            InitializeComponent();
            CacherControlesAjoutModif();
            lvCategorie.ItemsSource = donneesActuelles.LesCategories;
            
            DataContext = this;
        }
        #endregion
        
        #region evenements clicks boutons
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            lvCategorie.IsEnabled = true;

            if (lvCategorie.SelectedItem == null)
                return;

            string messageConfirmationSupp = "Êtes-vous sûr de vouloir supprimer cette catégorie ? \nTous les matériels dans les catégories vont également être supprimés";
            MessageBoxResult result = MessageBox.Show(messageConfirmationSupp, "Confirmation de suppression", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes && lvCategorie.SelectedItem is CategorieMateriel categorie)
            {
                ((CategorieMateriel)lvCategorie.SelectedItem).LesMateriels.Clear();
                categorie.Delete();

                donneesActuelles.LesCategories.Remove(categorie);

                lvCategorie.SelectedIndex = 0;
            }
        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderAjout.Visibility = Visibility.Visible;


            tblAnnonceAction.Text = "Ajout d'une catégorie";
            lvCategorie.SelectedItem = null;
            lvCategorie.IsEnabled = false;
        }

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderModification.Visibility = Visibility.Visible;


            tblAnnonceAction.Text = "Modification d'une catégorie";
            lvCategorie.IsEnabled = true;
        }

        private void btValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            new CategorieMateriel(0, tboxNomCategorie.Text).Create();

            CacherControlesAjoutModif();
            lvCategorie.IsEnabled = true;

            donneesActuelles.Refresh();

            lvCategorie.ItemsSource = donneesActuelles.LesCategories;
        }
        private void btValiderModification_Click(object sender, RoutedEventArgs e)
        {
            ((CategorieMateriel)lvCategorie.SelectedItem).Nom = tboxNomCategorie.Text;
            ((CategorieMateriel)lvCategorie.SelectedItem).Update();

            CacherControlesAjoutModif();
            lvCategorie.IsEnabled = true;

            donneesActuelles.Refresh();

            lvCategorie.ItemsSource = donneesActuelles.LesCategories;
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            CacherControlesAjoutModif();
            lvCategorie.IsEnabled = true;
        }
        #endregion

        #region Gestion affichage pour ajout/modification donnée

        private void AfficherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Visible;
            tboxNomCategorie.Visibility = Visibility.Visible;
            tblNomCategorie.Visibility = Visibility.Visible;

            btAnnuler.Visibility = Visibility.Visible;
        }

        private void CacherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Hidden;
            tboxNomCategorie.Visibility = Visibility.Hidden;
            tblNomCategorie.Visibility = Visibility.Hidden;

            btValiderAjout.Visibility = Visibility.Hidden;
            btValiderModification.Visibility = Visibility.Hidden;
            btAnnuler.Visibility = Visibility.Hidden;
        }

        #endregion

    }
}
