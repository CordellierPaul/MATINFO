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
        bool isCreating;
        EstAttribue attribution;
        public PopupAttribution()
        {
            InitializeComponent();
            isCreating = true;

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
        }

        public PopupAttribution(EstAttribue attribution)
        {
            InitializeComponent();
            this.attribution = attribution;
            isCreating = false;
            if (String.IsNullOrEmpty(attribution.Commentaire))
                tbCommentaire.Text = string.Empty;
            else
                tbCommentaire.Text = attribution.Commentaire;
            cbPersonnel.Items.Add(new ComboBoxItem()
            {
                Content = $"{attribution.UnPersonnel.Nom} {attribution.UnPersonnel.Prenom}"
            });
            cbMateriel.Items.Add(new ComboBoxItem()
            {
                Content = attribution.UnMateriel.Nom
            });
            cbPersonnel.IsEditable = false;
            cbMateriel.IsEditable = false;
            dpDate.SelectedDate = DateTime.Parse(attribution.DateAttribution);
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (isCreating)
                new EstAttribue(int.Parse(((ComboBoxItem)cbMateriel.SelectedItem).Name.Substring(8)), int.Parse(((ComboBoxItem)cbPersonnel.SelectedItem).Name.Substring(9)), ((DateTime)dpDate.SelectedDate).ToShortDateString(), tbCommentaire.Text).Create();
            else
            {
                attribution.Commentaire = tbCommentaire.Text;
                attribution.DateAttribution = ((DateTime)dpDate.SelectedDate).ToShortDateString();
                attribution.Update();
            }
            this.Close();
        }
    }
}
