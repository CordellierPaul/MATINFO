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
    /// Logique d'interaction pour PopupAttribution.xaml
    /// </summary>
    public partial class PopupAttribution : Window
    {
        bool doitCreerAttribuiton;
        EstAttribue attribution;

        /// <summary>
        /// Ce constructeur est appelé pour l'ajout d'une attribution
        /// </summary>
        public PopupAttribution()
        {
            InitializeComponent();
            doitCreerAttribuiton = true;

            foreach (Materiel materiel in donneesActuelles.LesMateriels)
            {
                cbMateriel.Items.Add(new ComboBoxItem()
                {
                    Content = materiel.Nom,
                    Name = $"Materiel{materiel.IDMateriel}"
                });
            }
            foreach (Personnel personnel in donneesActuelles.LePersonnel)
            {
                cbPersonnel.Items.Add(new ComboBoxItem()
                {
                    Content = $"{personnel.Nom} {personnel.Prenom}",
                    Name = $"Personnel{personnel.IDPersonnel}"
                });
            }
            dpDate.SelectedDate = DateTime.Today;

            attribution = new EstAttribue();
        }


        /// <summary>
        /// Ce constructeur est appelé pour la modification d'une attribution
        /// </summary>
        /// <param name="attribution"> Attribution qui doit être modifiée </param>
        public PopupAttribution(EstAttribue attribution)
        {
            InitializeComponent();
            this.attribution = attribution;
            doitCreerAttribuiton = false;
            if (string.IsNullOrEmpty(attribution.Commentaire))
                tbCommentaire.Text = string.Empty;
            else
                tbCommentaire.Text = attribution.Commentaire;
            cbPersonnel.Items.Add(new ComboBoxItem()
            {
                Content = $"{attribution.UnPersonnel!.Nom} {attribution.UnPersonnel.Prenom}"
            });
            cbMateriel.Items.Add(new ComboBoxItem()
            {
                Content = attribution.UnMateriel!.Nom
            });
            cbPersonnel.IsEnabled = false;
            cbMateriel.IsEnabled = false;
            dpDate.SelectedDate = DateTime.Parse(attribution.DateAttribution);
        }

        /// <summary>
        /// Déclenché lors du clic du bouton de création. Modifie ou ajoute l'attribution visible (en fonction de la variable doitCreerAttribuiton)
        /// </summary>
        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (doitCreerAttribuiton)
                new EstAttribue(int.Parse(((ComboBoxItem)cbMateriel.SelectedItem).Name.Substring(8)), int.Parse(((ComboBoxItem)cbPersonnel.SelectedItem).Name.Substring(9)), ((DateTime)dpDate.SelectedDate!).ToShortDateString(), tbCommentaire.Text).Create();
            else
            {
                attribution.Commentaire = tbCommentaire.Text;
                attribution.DateAttribution = ((DateTime)dpDate.SelectedDate!).ToShortDateString();
                attribution.Update();
            }
            this.Close();
        }
    }
}
