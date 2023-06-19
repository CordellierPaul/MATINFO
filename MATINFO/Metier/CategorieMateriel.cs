using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MATINFO.Metier
{
    public class CategorieMateriel : ICrud<CategorieMateriel>
    {
        public int IDCategorieMateriel { get; set; }
        public string Nom { get; set; }

        public List<Materiel> LesMateriels { get; set; }

        public CategorieMateriel(int idCategorieMateriel, string nom)
        {
            IDCategorieMateriel = idCategorieMateriel;
            Nom = nom;

            LesMateriels = new List<Materiel>();
        }

        public CategorieMateriel() : this(0, "") { }

        public void Create()
        {
            new AccesDonnees().SetData($"insert into categorie_materiel (nomcategorie) values ('{this.Nom}');");
        }
   
        public void Read()
        {
            // TODO: implement
        }
   
        public void Update()
        {
            new AccesDonnees().SetData($"update categorie_materiel set nomcategorie = '{this.Nom}' where idcategorie = {this.IDCategorieMateriel};");
        }
   
        public void Delete()
        {
            foreach (Materiel unMateriel in LesMateriels)
                unMateriel.Delete();

            new AccesDonnees().SetData($"delete from categorie_materiel where idcategorie = {IDCategorieMateriel};");
        }

        public ObservableCollection<CategorieMateriel> FindAll()
        {
            ObservableCollection<CategorieMateriel> lesCategories = new ObservableCollection<CategorieMateriel>();
            AccesDonnees accesBD = new AccesDonnees();
            string requete = "select idcategorie, nomcategorie from categorie_materiel ;";
            DataTable datas = accesBD.GetData(requete)!;
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    CategorieMateriel e = new CategorieMateriel(int.Parse(row["idcategorie"].ToString()!), (string)row["nomcategorie"]);
                    lesCategories.Add(e);
                }
            }
            return lesCategories;
        }
    }
}