using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATINFO.Metier
{
    public class GestionDonnees
    {
        #region declaration de variables
        public ObservableCollection<CategorieMateriel> LesCategories { get; set; }
        public ObservableCollection<Materiel> LesMateriels { get; set; }
        public ObservableCollection<Personnel> LePersonnel { get; set; }
        public ObservableCollection<EstAttribue> LesAttributions { get; set; }
        #endregion

        #region Constructeur
        public GestionDonnees()
        {
            Refresh();
        }
        #endregion
        /// <summary>
        /// Recharge toutes les ObservableCollection de GestionDonnees avec la base de données. Utilisé dans le constructeur
        /// pour tout créer et dans le code des fenêtres pour que tous les id générés concordent.
        /// </summary>
        public void Refresh()
        {
            LesCategories = new CategorieMateriel().FindAll();
            LesMateriels = new Materiel().FindAll();
            LePersonnel = new Personnel().FindAll();
            LesAttributions = new EstAttribue().FindAll();

            // Liaison CategorieMateriel / Materiel
            foreach (CategorieMateriel uneCategorie in LesCategories)
            {
                foreach (Materiel unMateriel in LesMateriels)
                {
                    if (uneCategorie.IDCategorieMateriel == unMateriel.IDCategorieMateriel)
                    {
                        uneCategorie.LesMateriels.Add(unMateriel);
                        unMateriel.UneCategorieMateriel = uneCategorie;
                    }
                }
            }

            foreach (EstAttribue uneAttribution in LesAttributions)
            {
                // Liaison Materiel / Attribution
                foreach (Materiel unMateriel in LesMateriels)
                {
                    if (uneAttribution.IDMateriel == unMateriel.IDMateriel)
                    {
                        unMateriel.LesAttributions.Add(uneAttribution);
                    }
                }

                // Liaison Personnel / Attribution
                foreach (Personnel unPersonnel in LePersonnel)
                {
                    if (uneAttribution.IDPersonnel == unPersonnel.IDPersonnel)
                    {
                        unPersonnel.LesAttributions.Add(uneAttribution);
                    }
                }
            }
        }
    }
}
