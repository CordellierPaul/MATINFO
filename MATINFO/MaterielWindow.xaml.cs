using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using MATINFO.Metier;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour MaterielWindow.xaml
    /// </summary>
    public partial class MaterielWindow : Window
    {
        public MaterielWindow()
        {
            InitializeComponent();
            CacherControlesAjoutModif();
            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;
            foreach (CategorieMateriel categorie in donneesActuelles.LesCategories)
            {
                cbCategorie.Items.Add(new ComboBoxItem()
                {
                    Content = categorie.Nom,
                    Name = $"Categorie{categorie.IDCategorieMateriel}"
                });
            }
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
        /// Lancé au clic du bouton qui supprime la dernier matériel sélectionné.
        /// </summary>
        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem == null)
                return;

            MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce matériel ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes && lvMateriel.SelectedItem is Materiel materiel)
            {
                donneesActuelles.LesMateriels.Remove(materiel);

                materiel.Delete();

                lvMateriel.SelectedIndex = 0;
            }
        }


        /// <summary>
        /// Lancé au clic du bouton pour ajouter un matériel. Affiche les champs qui permettre d'ajouter
        /// et de modifier les matériels, et le bouton de validation d'ajout.
        /// </summary>
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderAjout.Visibility = Visibility.Visible;
            btValiderModification.Visibility = Visibility.Hidden;


            tblAnnonceAction.Text = "Ajout d'un materiel";
            lvMateriel.SelectedItem = null;
            lvMateriel.IsEnabled = false;
        }

        /// <summary>
        /// Lancé au clic du bouton pour modifier un matériel. Affiche les champs qui permettre d'ajouter
        /// et de modifier les matériels, et le bouton de validation de modification.
        /// </summary>
        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderModification.Visibility = Visibility.Visible;
            btValiderAjout.Visibility = Visibility.Hidden;


            tblAnnonceAction.Text = "Modification d'un materiel";
            lvMateriel.IsEnabled = true;
        }

        /// <summary>
        /// Ajoute le matériel marqué dans les champs d'ajout et de modification.
        /// </summary>
        private void btValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            new Materiel(0, int.Parse(((ComboBoxItem)cbCategorie.SelectedItem).Name.Substring(9)), tboxNom.Text, tboxCodeBarre.Text, tboxReference.Text).Create();

            CacherControlesAjoutModif();
            lvMateriel.IsEnabled = true;

            donneesActuelles.Refresh();

            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;
        }

        /// <summary>
        /// Modifie le matériel sélectionné en fonction des champs d'ajout et de modification.
        /// </summary>
        private void btValiderModification_Click(object sender, RoutedEventArgs e)
        {
            ((Materiel)lvMateriel.SelectedItem).IDCategorieMateriel = int.Parse(((ComboBoxItem)cbCategorie.SelectedItem).Name.Substring(9));
            ((Materiel)lvMateriel.SelectedItem).Nom = tboxNom.Text;
            ((Materiel)lvMateriel.SelectedItem).CodeBarre = tboxCodeBarre.Text;
            ((Materiel)lvMateriel.SelectedItem).Reference = tboxReference.Text;
            ((Materiel)lvMateriel.SelectedItem).Update();

            CacherControlesAjoutModif();
            lvMateriel.IsEnabled = true;

            donneesActuelles.Refresh();

            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;
        }

        /// <summary>
        /// Déclenché au clic du bouton "Annuler", visible uniquement lorsque les champs d'ajout et de modification.
        /// Cache ces champs, et le texte qui va avec.
        /// </summary>
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            CacherControlesAjoutModif();
            lvMateriel.IsEnabled = true;
        }


        /// <summary>
        /// Affiche les champs d'ajout, de modification, les boutons et les textes qui vont avec.
        /// </summary>
        private void AfficherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Visible;
            tboxNom.Visibility = Visibility.Visible;
            tblNom.Visibility = Visibility.Visible;
            tboxCodeBarre.Visibility = Visibility.Visible;
            tblCodeBarre.Visibility = Visibility.Visible;
            tboxReference.Visibility = Visibility.Visible;
            tblReference.Visibility = Visibility.Visible;
            tblCategorie.Visibility = Visibility.Visible;
            cbCategorie.Visibility = Visibility.Visible;

            btAnnuler.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Cache les champs d'ajout, de modification, les boutons et les textes qui vont avec.
        /// </summary>
        private void CacherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Hidden;
            tboxNom.Visibility = Visibility.Hidden;
            tblNom.Visibility = Visibility.Hidden;
            tboxCodeBarre.Visibility = Visibility.Hidden;
            tblCodeBarre.Visibility = Visibility.Hidden;
            tboxReference.Visibility = Visibility.Hidden;
            tblReference.Visibility = Visibility.Hidden;
            tblCategorie.Visibility = Visibility.Hidden;
            cbCategorie.Visibility = Visibility.Hidden;

            btValiderAjout.Visibility = Visibility.Hidden;
            btValiderModification.Visibility = Visibility.Hidden;
            btAnnuler.Visibility = Visibility.Hidden;
        }
    }
}
