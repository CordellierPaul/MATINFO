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

            lvPersonnel.ItemsSource = donneesActuelles.LePersonnel;

            DataContext = this;
        }

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
    }
}
