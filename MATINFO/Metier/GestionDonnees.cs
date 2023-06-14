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
        public ObservableCollection<CategorieMateriel> LesCategories { get; set; }
        public ObservableCollection<Materiel> LesMateriels { get; set; }
        public ObservableCollection<Personnel> LePersonnel { get; set; }
        public ObservableCollection<EstAttribue> LesAttributions { get; set; }

        public GestionDonnees()
        {

            //LesCategories = new CategorieMateriel().FindAll();
            //LesMateriels = new Materiel().FindAll();
            //LePersonnel = new Personnel().FindAll();
            //LesAttributions = new EstAttribue().FindAll();

            LesCategories = new ObservableCollection<CategorieMateriel>()
            {
                new CategorieMateriel(1, "Catégorie1"),
                new CategorieMateriel(2, "Catégorie2"),
                new CategorieMateriel(3, "Catégorie3")
            };
            LesMateriels = new ObservableCollection<Materiel>()
            {
                new Materiel(1, 1, "FOIFHOIZE", "Nom", "Réference")
            };
            LePersonnel = new ObservableCollection<Personnel>()
            {
                new Personnel(1, "Jean", "Prénom", "abirefgbli@gmail.com")
            };
            LesAttributions = new ObservableCollection<EstAttribue>();
        }
    }
}
