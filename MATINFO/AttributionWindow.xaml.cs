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
using System.Windows.Media.Imaging;
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

            lvAttribution.ItemsSource = donneesActuelles.LesCategories;

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
    }
}
