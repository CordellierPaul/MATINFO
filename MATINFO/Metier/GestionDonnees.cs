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
        public ObservableCollection<Personnel> LesPersonnels { get; set; }
        public ObservableCollection<EstAttribue> LesAttributions { get; set; }

        public GestionDonnees()
        {
            //LesCategories = CategorieMateriel.FindAll();
            //LesMateriels = Materiel.FindAll();
            //LesPersonnels = Personnel.FindAll();
            //LesAttributions = EstAttribue.FindAll();

            LesCategories = new ObservableCollection<CategorieMateriel>()
            {
                new CategorieMateriel(1, "Catégorie1"),
                new CategorieMateriel(2, "Catégorie2"),
                new CategorieMateriel(3, "Catégorie3")
            };
            LesMateriels = new ObservableCollection<Materiel>();
            LesPersonnels = new ObservableCollection<Personnel>();
            LesAttributions = new ObservableCollection<EstAttribue>();
        }
    }
}
