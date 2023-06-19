using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MATINFO.Metier;
using Microsoft.Win32;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour CategorieWindow.xaml
    /// </summary>
    public partial class CategorieWindow : Window
    {
        public CategorieWindow()
        {
            InitializeComponent();
            CacherControlesAjoutModif();
            lvCategorie.ItemsSource = donneesActuelles.LesCategories;
            
            DataContext = this;
        }
        /// <summary>
        /// Lancé au clic du bouton en haut à gauche de l'écran. On revient à l'écran d'accueil de l'application (MainWindow.xaml).
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        /// <summary>
        /// Lancé au clic du bouton qui supprime la dernière catégorie sélectionnée.
        /// </summary>
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

        /// <summary>
        /// Lancé au clic du bouton pour ajouter une catégoire. Affiche les champs qui permettre d'ajouter
        /// et de modifier les catégorie, et le bouton de validation d'ajout.
        /// </summary>
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderAjout.Visibility = Visibility.Visible;


            tblAnnonceAction.Text = "Ajout d'une catégorie";
            lvCategorie.SelectedItem = null;
            lvCategorie.IsEnabled = false;
        }

        /// <summary>
        /// Lancé au clic du bouton pour modifier une catégoire. Affiche les champs qui permettre d'ajouter
        /// et de modifier les catégoire, et le bouton de validation de modification.
        /// </summary>
        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderModification.Visibility = Visibility.Visible;


            tblAnnonceAction.Text = "Modification d'une catégorie";
            lvCategorie.IsEnabled = true;
        }

        /// <summary>
        /// Ajoute la catégorie marquée dans les champs d'ajout et de modification.
        /// </summary>
        private void btValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            new CategorieMateriel(0, tboxNomCategorie.Text).Create();

            CacherControlesAjoutModif();
            lvCategorie.IsEnabled = true;

            donneesActuelles.Refresh();

            lvCategorie.ItemsSource = donneesActuelles.LesCategories;
        }

        /// <summary>
        /// Modifie la catégorie sélectionnée en fonction des champs d'ajout et de modification.
        /// </summary>
        private void btValiderModification_Click(object sender, RoutedEventArgs e)
        {
            ((CategorieMateriel)lvCategorie.SelectedItem).Nom = tboxNomCategorie.Text;
            ((CategorieMateriel)lvCategorie.SelectedItem).Update();

            CacherControlesAjoutModif();
            lvCategorie.IsEnabled = true;

            donneesActuelles.Refresh();

            lvCategorie.ItemsSource = donneesActuelles.LesCategories;
        }

        /// <summary>
        /// Déclenché au clic du bouton "Annuler", visible uniquement lorsque les champs d'ajout et de modification.
        /// Cache ces champs, et le texte qui va avec.
        /// </summary>
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            CacherControlesAjoutModif();
            lvCategorie.IsEnabled = true;
        }

        /// <summary>
        /// Affiche les champs d'ajout, de modification, les boutons et les textes qui vont avec.
        /// </summary>
        private void AfficherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Visible;
            tboxNomCategorie.Visibility = Visibility.Visible;
            tblNomCategorie.Visibility = Visibility.Visible;

            btAnnuler.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Cache les champs d'ajout, de modification, les boutons et les textes qui vont avec.
        /// </summary>
        private void CacherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Hidden;
            tboxNomCategorie.Visibility = Visibility.Hidden;
            tblNomCategorie.Visibility = Visibility.Hidden;

            btValiderAjout.Visibility = Visibility.Hidden;
            btValiderModification.Visibility = Visibility.Hidden;
            btAnnuler.Visibility = Visibility.Hidden;
        }
    }
}
