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
            refresh();
        }
        #endregion
        public void refresh()
        {
            lvCategorieMateriel.ItemsSource = donneesActuelles.LesCategories;
            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;
            lvAttribution.ItemsSource = donneesActuelles.LesAttributions;
            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;

            lvCategorieMateriel.SelectAll();
            lvMateriel.SelectedIndex = -1;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvAttribution.ItemsSource);
            view.Filter = FiltreAttribution;

            view = (CollectionView)CollectionViewSource.GetDefaultView(lvMateriel.ItemsSource);
            view.Filter = FiltreMateriel;

            DataContext = this;
        }

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

        private void btAfficherTout_Click(object sender, RoutedEventArgs e)
        {
            lvCategorieMateriel.SelectAll();
            lvMateriel.SelectedIndex = -1;
            lvAttribution.SelectedIndex = -1;
            lvPersonnel.SelectedIndex = -1;
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
            lvMateriel.SelectAll();
        }
        #endregion

        #endregion

        #region filtres
        /// <summary>
        /// Si cette fonction renvoie true, le matériel est affiché
        /// </summary>
        /// <param name="item">Un matériel dans la liste view</param>
        /// <returns>true si materielDansLaListe à l'idCategorie qui correspond à une des/de la CategorieMateriel sélectionée(s)</returns>
        private bool FiltreMateriel(object item)
        {
            Materiel materielDansLaListe = (Materiel)item;

            foreach (CategorieMateriel uneCategorie in lvCategorieMateriel.SelectedItems)
            {
                if (materielDansLaListe.IDCategorieMateriel == uneCategorie.IDCategorieMateriel)
                    return true;    // L'idCategorieMateriel correspond au matériel
            }

            return false;   // Ici il n'y a pas d'idPersonnel qui correspond
        }

        /// <summary>
        /// Si cette fonction renvoie true, l'attribution est affichée
        /// </summary>
        /// <param name="item">Une attribution dans la liste view</param>
        /// <returns>true si attributDansLaListe à l'idMateriel et à l'idPersonnel qui correspondent aux éléments Materiel
        /// et Personnel. Aucune vérification ne sera faite si rien n'est sélectionné pour chaque élement </returns>
        private bool FiltreAttribution(object item)
        {
            EstAttribue attributDansLaListe = (EstAttribue)item;

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

            if (lvPersonnel.SelectedItem != null)   // Si quelque chose est séléctionné lvPersonnel
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

        private void btAjouterAttrib_Click(object sender, RoutedEventArgs e)
        {
            new PopupAttribution().ShowDialog();
            donneesActuelles.Refresh();
            lvAttribution.ItemsSource = donneesActuelles.LesAttributions;
            refresh();
        }

        private void btModifierAttrib_Click(object sender, RoutedEventArgs e)
        {
            if (lvAttribution.SelectedItem == null)
                MessageBox.Show("Veuillez selectionner une attribution a modiffier", "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            else
            {
                new PopupAttribution((EstAttribue)lvAttribution.SelectedItem).ShowDialog();
                donneesActuelles.Refresh();
                lvAttribution.ItemsSource = donneesActuelles.LesAttributions;
            }
            refresh();
        }
    }
}
