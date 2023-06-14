﻿using System.Windows;
using MATINFO.Metier;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour CategorieRep.xaml
    /// </summary>
    public partial class CategorieWindow : Window
    {
        public CategorieWindow()
        {
            InitializeComponent();

            lvCategorie.ItemsSource = donneesActuelles.LesCategories;

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
            if (lvCategorie.SelectedItem != null)
            {
                string messageConfirmationSupp = "Êtes-vous sûr de vouloir supprimer cette catégorie ? \nTous les matériels dans les catégories vont également être supprimés";
                MessageBoxResult result = MessageBox.Show(messageConfirmationSupp, "Confirmation de suppression", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes && lvCategorie.SelectedItem is CategorieMateriel categorie)
                {
                    donneesActuelles.LesCategories.Remove(categorie);

                    categorie.Delete();

                    lvCategorie.SelectedIndex = 0;
                }
            }
        }
    }
}
