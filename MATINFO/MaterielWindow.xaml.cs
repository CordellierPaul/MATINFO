using System.Windows;
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

            lvMateriel.ItemsSource = donneesActuelles.LesMateriels;

            DataContext = this;
        }

        #region Gesiton des évenements
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void btSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce matériel ?", "Confirmation de suppression", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes && lvMateriel.SelectedItem is Materiel materiel)
                {
                    donneesActuelles.LesMateriels.Remove(materiel);

                    materiel.Delete();

                    lvMateriel.SelectedIndex = 0;
                }
            }
        }
        #endregion
    }
}
