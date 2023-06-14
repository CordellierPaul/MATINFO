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

        public CategorieMateriel() : this(1, "") { }


        #region Implementation de l'interface CRUD
        public void Create()
        {
            // TODO: implement
        }
   
        public void Read()
        {
            // TODO: implement
        }
   
        public void Update()
        {
            // TODO: implement
        }
   
        public void Delete()
        {
            // Suppression récurisve du matériel
            foreach (Materiel unMateriel in LesMateriels)
                unMateriel.Delete();
            
            LesMateriels.Clear();

            AccesDonnees accesBD = new AccesDonnees();
            string requete = "delete from categorie_materiel where idcategorie = " + IDCategorieMateriel + " ;";
            accesBD.SetData(requete);
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
        #endregion

        #region Classes générées
        /// <pdGenerated>default getter</pdGenerated>
        public List<Materiel> GetMateriel()
        {
            if (LesMateriels == null)
                LesMateriels = new List<Materiel>();
            return LesMateriels;
        }
   
        /// <pdGenerated>default setter</pdGenerated>
        public void SetMateriel(List<Materiel> newMateriel)
        {
            RemoveAllMateriel();
            foreach (Materiel oMateriel in newMateriel)
                AddMateriel(oMateriel);
        }
   
        /// <pdGenerated>default Add</pdGenerated>
        public void AddMateriel(Materiel newMateriel)
        {
            if (newMateriel == null)
                return;
            if (this.LesMateriels == null)
                this.LesMateriels = new List<Materiel>();
            if (!this.LesMateriels.Contains(newMateriel))
                this.LesMateriels.Add(newMateriel);
        }
   
        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveMateriel(Materiel oldMateriel)
        {
            if (oldMateriel == null)
                return;
            if (this.LesMateriels != null)
                if (this.LesMateriels.Contains(oldMateriel))
                this.LesMateriels.Remove(oldMateriel);
        }
   
        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllMateriel()
        {
            if (LesMateriels != null)
                LesMateriels.Clear();
        }
        #endregion
    }
}