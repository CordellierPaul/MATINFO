using MATINFO.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour AttributionWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructeur
        public MainWindow()
        {
            InitializeComponent();

            new AccesDonnees().OpenConnection();

            lvCategorieMateriel.ItemsSource = donneesActuelles.LesCategories;
            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;
            lvAttribution.ItemsSource = donneesActuelles.LesAttributions;
            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvAttribution.ItemsSource);
            view.Filter = FiltreAttribution;

            view = (CollectionView)CollectionViewSource.GetDefaultView(lvMateriel.ItemsSource);
            view.Filter = FiltreMateriel;

            DataContext = this;
        }
        #endregion

        #region Evenements

        #region Clicks boutons
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lvAttribution.SelectedItem == null)
                return;

            string messageConfirmationSupp = "Êtes-vous sûr de vouloir supprimer cette attribution ?";
            MessageBoxResult result = MessageBox.Show(messageConfirmationSupp, "Confirmation de suppression", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes && lvAttribution.SelectedItem is EstAttribue categorie)
            {
                categorie.Delete();

                donneesActuelles.LesAttributions.Remove(categorie);

                lvAttribution.SelectedIndex = 0;
            }
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
        #endregion

        #region Selection change
        private void lvMaterielPersonnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvAttribution.ItemsSource).Refresh();
        }

        private void lvCategorieMateriel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvMateriel.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvAttribution.ItemsSource).Refresh();
        }
        #endregion

        #endregion

        #region filtres
        // Si cette fonction renvoie true, le matériel est affiché
        private bool FiltreMateriel(object item)
        {
            Materiel materielDansLaListe = (Materiel)item;
            // true sera renvoyé si materielDansLaListe à l'idCategorie qui correspond
            // à une des CategorieMateriel sélectionées

            if (lvCategorieMateriel.SelectedItem == null)
                return true;    // Si rien n'est sélectionné dans lvCategorieMateriel, tout est affiché

            foreach (CategorieMateriel uneCategorie in lvCategorieMateriel.SelectedItems)
            {
                if (materielDansLaListe.IDCategorieMateriel == uneCategorie.IDCategorieMateriel)
                    return true;    // L'idCategorieMateriel correspont au matériel
            }

            return false;   // Ici il n'y a pas d'idPersonnel qui correspond
        }

        // Si cette fonction renvoie true, l'attribution est affichée.
        private bool FiltreAttribution(object item)
        {
            EstAttribue attributDansLaListe = (EstAttribue)item;
            // true sera renvoyé si attributDansLaListe à l'idMateriel et à l'idPersonnel qui correspondent
            // aux éléments Materiel et Personnel. 

            if (lvMateriel.SelectedItem != null)    // Si quelque chose est séléctionné lvMateriel
            {
                bool memeIdMaterielTrouve = false;

                foreach (Materiel unMateriel in lvMateriel.SelectedItems)
                {
                    if (attributDansLaListe.IDMateriel == unMateriel.IDMateriel)
                    {
                        memeIdMaterielTrouve = true;
                        break;
                    }
                }

                if (!memeIdMaterielTrouve)
                    return false;   // Ici il n'y a pas d'idMateriel qui correspond
            }

            if (lvPersonnel.SelectedItem != null)    // Si quelque chose est séléctionné lvPersonnel
            {
                foreach (Personnel unPersonnel in lvPersonnel.SelectedItems)
                {
                    if (attributDansLaListe.IDPersonnel == unPersonnel.IDPersonnel)
                        return true;    // L'idPersonnel et l'idMateriel correspondent avec l'objet attributDansLaListe
                }

                return false;   // Ici il n'y a pas d'idPersonnel qui correspond
            }

            return true;    // Rien n'est sélectionné
        }
        #endregion
    }
}
