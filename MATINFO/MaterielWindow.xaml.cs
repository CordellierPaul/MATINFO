using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

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

        #region evenements clicks boutons
        
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderAjout.Visibility = Visibility.Visible;
            btValiderModification.Visibility = Visibility.Hidden;


            tblAnnonceAction.Text = "Ajout d'un materiel";
            lvMateriel.SelectedItem = null;
            lvMateriel.IsEnabled = false;
        }

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherControlesAjoutModif();
            btValiderModification.Visibility = Visibility.Visible;
            btValiderAjout.Visibility = Visibility.Hidden;


            tblAnnonceAction.Text = "Modification d'un materiel";
            lvMateriel.IsEnabled = true;
        }

        private void btValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            new Materiel(0, int.Parse(((ComboBoxItem)cbCategorie.SelectedItem).Name.Substring(9)), tboxNom.Text, tboxCodeBarre.Text, tboxReference.Text).Create();

            CacherControlesAjoutModif();
            lvMateriel.IsEnabled = true;

            donneesActuelles.Refresh();

            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;
        }
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

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            CacherControlesAjoutModif();
            lvMateriel.IsEnabled = true;
        }
        #endregion
        #region Gestion affichage pour ajout/modification donnée

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

        #endregion

    }
}
