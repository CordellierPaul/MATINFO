using System.Windows;
using MATINFO.Metier;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour PersonnelRep.xaml
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
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

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

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {

            AfficherControlesAjoutModif();
            btValiderAjout.Visibility = Visibility.Visible;
            btValiderModification.Visibility = Visibility.Hidden;

            tblAnnonceAction.Text = "Ajout de personnel";
            lvPersonnel.IsEnabled = false;
        }
        

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderModification.Visibility = Visibility.Visible;
            btValiderAjout.Visibility = Visibility.Hidden;


            tblAnnonceAction.Text = "Modification de personnel";
            lvPersonnel.IsEnabled = true;
        }

        private void btValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            new Personnel(0, tboxNom.Text, tboxPrenom.Text, tboxEmail.Text).Create();

            CacherControlesAjoutModif();
            lvPersonnel.IsEnabled = true;

            donneesActuelles.Refresh();

            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;
        }
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

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            CacherControlesAjoutModif();
            lvPersonnel.IsEnabled = true;
        }
        #endregion

        #region Gestion affichage pour ajout/modification donnée

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
