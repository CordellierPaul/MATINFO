using System.Windows;
using MATINFO.Metier;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour PersonnelWindow.xaml
    /// </summary>
    public partial class PersonnelWindow : Window
    {
        public PersonnelWindow()
        {
            InitializeComponent();
            CacherControlesAjoutModif();
            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;
        }

        #region evenements clicks boutons
        /// <summary>
        /// Lancé au clic du bouton en haut à gauche de l'écran. On revient à l'écran d'accueil de l'application (MainWindow.xaml).
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        /// <summary>
        /// Lancé au clic du bouton qui supprime le dernier personnel sélectionné.
        /// </summary>
        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lvPersonnel.SelectedItem == null)
                return;

            MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce personnel ?", "Confirmation de suppression", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes && lvPersonnel.SelectedItem is Personnel personnel)
            {
                donneesActuelles.LePersonnel.Remove(personnel);

                personnel.Delete();

                lvPersonnel.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Lancé au clic du bouton pour ajouter un personnel. Affiche les champs qui permettre d'ajouter
        /// et de modifier le personnel, et le bouton de validation d'ajout.
        /// </summary>
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {

            AfficherControlesAjoutModif();
            btValiderAjout.Visibility = Visibility.Visible;
            btValiderModification.Visibility = Visibility.Hidden;

            tblAnnonceAction.Text = "Ajout de personnel";
            lvPersonnel.IsEnabled = false;
        }

        /// <summary>
        /// Lancé au clic du bouton pour modifier un personnel. Affiche les champs qui permettre d'ajouter
        /// et de modifier le personnel, et le bouton de validation de modification.
        /// </summary>
        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderModification.Visibility = Visibility.Visible;
            btValiderAjout.Visibility = Visibility.Hidden;


            tblAnnonceAction.Text = "Modification de personnel";
            lvPersonnel.IsEnabled = true;
        }

        /// <summary>
        /// Ajoute le personnel marqué dans les champs d'ajout et de modification.
        /// </summary>
        private void btValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            new Personnel(0, tboxNom.Text, tboxPrenom.Text, tboxEmail.Text).Create();

            CacherControlesAjoutModif();
            lvPersonnel.IsEnabled = true;

            donneesActuelles.Refresh();

            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;
        }

        /// <summary>
        /// Modifie le personnel sélectionné en fonction des champs d'ajout et de modification.
        /// </summary>
        private void btValiderModification_Click(object sender, RoutedEventArgs e)
        {
            ((Personnel)lvPersonnel.SelectedItem).Nom = tboxNom.Text;
            ((Personnel)lvPersonnel.SelectedItem).Prenom = tboxPrenom.Text;
            ((Personnel)lvPersonnel.SelectedItem).Email = tboxEmail.Text;
            ((Personnel)lvPersonnel.SelectedItem).Update();

            CacherControlesAjoutModif();
            lvPersonnel.IsEnabled = true;

            donneesActuelles.Refresh();

            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;
        }

        /// <summary>
        /// Déclenché au clic du bouton "Annuler", visible uniquement lorsque les champs d'ajout et de modification.
        /// Cache ces champs.
        /// </summary>
        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            CacherControlesAjoutModif();
            lvPersonnel.IsEnabled = true;
        }
        #endregion

        #region Gestion affichage pour ajout/modification donnée

        /// <summary>
        /// Affiche les champs d'ajout, de modification, les boutons et les textes qui vont avec.
        /// </summary>
        private void AfficherControlesAjoutModif()
        {
            tblAnnonceAction.Visibility = Visibility.Visible;
            tboxNom.Visibility = Visibility.Visible;
            tblNom.Visibility = Visibility.Visible;
            tboxPrenom.Visibility = Visibility.Visible;
            tblPrenom.Visibility = Visibility.Visible;
            tboxEmail.Visibility = Visibility.Visible;
            tblEmail.Visibility = Visibility.Visible;

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
            tboxPrenom.Visibility = Visibility.Hidden;
            tblPrenom.Visibility = Visibility.Hidden;
            tboxEmail.Visibility = Visibility.Hidden;
            tblEmail.Visibility = Visibility.Hidden;

            btValiderAjout.Visibility = Visibility.Hidden;
            btValiderModification.Visibility = Visibility.Hidden;
            btAnnuler.Visibility = Visibility.Hidden;
        }

        #endregion
    }
}
