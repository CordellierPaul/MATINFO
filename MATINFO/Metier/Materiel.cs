/***********************************************************************
 * Module:  Materiel.cs
 * Author:  cordellp
 * Purpose: Definition of the Class Materiel
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MATINFO.Metier
{
    public class Materiel : ICrud
    {
        public int IDMateriel { get; set; }
        public int IDCategorieMateriel { get; set; }
        public string CodeBarre { get; set; }
        public string Nom { get; set; }
        public string Reference { get; set; }

        public List<EstAttribue> EstAttribue { get; set; }

        public Materiel(int idMateriel, int idCategorieMateriel, string codeBarre, string nom, string reference)
        {
            IDMateriel = idMateriel;
            IDCategorieMateriel = idCategorieMateriel;
            CodeBarre = codeBarre;
            Nom = nom;
            Reference = reference;
        }
   
        public void Create()
        {
            // TODO: implement
        }
   
        public ICrud? Read()
        {
            // TODO: implement
            return null;
        }
   
        public void Update()
        {
            // TODO: implement
        }
   
        public void Delete()
        {
            // TODO: implement
        }

        public static ObservableCollection<Materiel> FindAll()
        {
            ObservableCollection<Materiel> leMateriel = new ObservableCollection<Materiel>();
            AccesDonnees accesBD = new AccesDonnees();
            string requete = "select idmateriel, idcategorie, codebarreinventaire, nommateriel, referenceconstructeurmateriel from materiel ;";
            DataTable datas = accesBD.GetData(requete)!;
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel e = new Materiel(int.Parse(row["idmateriel"].ToString()!), int.Parse(row["idcategorie"].ToString()!), (string)row["codebarreinventaire"], (string)row["nommateriel"], (string)row["referenceconstructeurmateriel"]);
                    leMateriel.Add(e);
                }
            }
            return leMateriel;
        }
    }
}