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
    public partial class AttributionWindow : Window
    {
        public AttributionWindow()
        {
            InitializeComponent();

            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;
            lvAttribution.ItemsSource = donneesActuelles.LesAttributions;
            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvAttribution.ItemsSource);
            view.Filter = FiltreAttribution;

            DataContext = this;
        }

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

        // Si cette fonction renvoie true, l'attribution est affichée.
        private bool FiltreAttribution(object item)
        {
            EstAttribue attributDansLaListe = (EstAttribue)item;
            // true sera renvoyé si attributDansLaListe à l'idMateriel et à l'idPersonnel correspondent
            // à un des éléments Materiel et Personnel. 

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

            return true;
        }
    }
}
