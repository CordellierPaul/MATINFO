using System.Windows;
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

            lvCategorie.ItemsSource = donneesActuelles.LesCategories;

            DataContext = this;

            CacherControlesAjoutModif();
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
                categorie.Delete();

                donneesActuelles.LesCategories.Remove(categorie);

                lvCategorie.SelectedIndex = 0;
            }
        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();

            tblAnnonceAction.Text = "Ajout d'une catégorie";
            btValider.Content = "Valider ajout";

            lvCategorie.SelectedItem = null;
            lvCategorie.IsEnabled = false;
        }

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();

            tblAnnonceAction.Text = "Modification d'une catégorie";
            btValider.Content = "Valider modification";
            lvCategorie.IsEnabled = true;
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            CacherControlesAjoutModif();
            lvCategorie.IsEnabled = true;
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

            btValider.Visibility = Visibility.Visible;
            btAnnuler.Visibility = Visibility.Visible;
        }

        private void CacherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Hidden;
            tboxNomCategorie.Visibility = Visibility.Hidden;
            tblNomCategorie.Visibility = Visibility.Hidden;

            btValider.Visibility = Visibility.Hidden;
            btAnnuler.Visibility = Visibility.Hidden;
        }

        #endregion

    }
}
