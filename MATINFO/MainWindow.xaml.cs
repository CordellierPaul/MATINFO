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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructeur
        public MainWindow()
        {
            InitializeComponent();
            Refresh();

            DataContext = this;

            new Materiel(0, 5, "Test1", "Test2", "Test3").Delete();
            new CategorieMateriel(0, "TestNomDeLaCategorie").Delete();
        }
        #endregion

        /// <summary>
        /// Rafraîchit les List view pour s'assurer que ce qui est affiché à l'écran concorde avec la base de données.
        /// </summary>
        public void Refresh()
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
        }

        #region Evenements

        #region Clicks boutons
        /// <summary>
        /// Lancé au clic du bouton en haut à gauche de l'écran. On ferme et rouvre cette fenêtere, car c'est celle d'accueil.
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        /// <summary>
        /// Pour supprimer une attribution.
        /// </summary>
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

        /// <summary>
        /// Pour ouvrir la fenêtere d'affichage des catégories.
        /// </summary>
        private void btCategorieWindow_Click(object sender, RoutedEventArgs e)
        {
            CategorieWindow categorieWindow = new CategorieWindow();
            Close();
            categorieWindow.Show();
        }

        /// <summary>
        /// Pour ouvrir la fenêtere d'affichage des matériels.
        /// </summary>
        private void btMaterielWindow_Click(object sender, RoutedEventArgs e)
        {
            MaterielWindow materielWindow = new MaterielWindow();
            Close();
            materielWindow.Show();
        }

        /// <summary>
        /// Pour ouvrir la fenêtere d'affichage du personnel.
        /// </summary>
        private void btPersonnelWindow_Click(object sender, RoutedEventArgs e)
        {
            PersonnelWindow personnelWindow = new PersonnelWindow();
            Close();
            personnelWindow.Show();
        }

        /// <summary>
        /// Activé par le bouton en haut à droite de l'écran. Sert à remplir toutes les ListView de l'application de
        /// toutes les données de la base.
        /// </summary>
        private void btAfficherTout_Click(object sender, RoutedEventArgs e)
        {
            lvCategorieMateriel.SelectAll();
            lvMateriel.SelectedIndex = -1;
            lvAttribution.SelectedIndex = -1;
            lvPersonnel.SelectedIndex = -1;
        }
        #endregion

        #region Selection change
        /// <summary>
        /// Déclenché lorsque la sélection de la ListView du personnel ou du matériel change.
        /// Rafraîchit la ListView des attributions.
        /// </summary>
        private void lvMaterielPersonnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvAttribution.ItemsSource).Refresh();
        }

        /// <summary>
        /// Déclenché lorsque la sélection de la ListView des catégories du matériel change.
        /// Rafraîchit la ListView du matériel et des attributions.
        /// </summary>
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
        /// Si cette fonction renvoie true, le matériel est affiché.
        /// </summary>
        /// <param name="item">Un matériel dans la ListView</param>
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
        /// <param name="item">Une attribution dans la ListView</param>
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

        /// <summary>
        /// Pour le bouton qui sert à ajouter une attribution. Ouvre la fenêtre correspondante.
        /// </summary>
        private void btAjouterAttrib_Click(object sender, RoutedEventArgs e)
        {
            new PopupAttribution().ShowDialog();
            donneesActuelles.Refresh();
            lvAttribution.ItemsSource = donneesActuelles.LesAttributions;
            Refresh();
        }

        /// <summary>
        /// Pour le bouton qui sert à modifier une attribution. Ouvre la fenêtre correspondante.
        /// </summary>
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
            Refresh();
        }
    }
}
