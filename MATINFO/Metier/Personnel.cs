/***********************************************************************
 * Module:  Personnel.cs
 * Author:  cordellp
 * Purpose: Definition of the Class Personnel
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MATINFO.Metier
{
    public class Personnel : ICrud
    {
        public int IDPersonnel { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string EMail { get; set; }

        public List<EstAttribue> LesAttributions { get; set; }

        public Personnel(int idPersonnel, string nom, string prenom, string eMail)
        {
            IDPersonnel = idPersonnel;
            Nom = nom;
            Prenom = prenom;
            EMail = eMail;
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

        public static ObservableCollection<Personnel> FindAll()
        {
            ObservableCollection<Personnel> lePersonnel = new ObservableCollection<Personnel>();
            AccesDonnees accesBD = new AccesDonnees();
            string requete = "select idpersonnel, nompersonnel, prenompersonnel, emailpersonnel from personnel ;";
            DataTable datas = accesBD.GetData(requete)!;
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Personnel e = new Personnel(int.Parse(row["idpersonnel"].ToString()!), (string)row["nompersonnel"], (string)row["prenompersonnel"], (string)row["emailpersonnel"]);
                    lePersonnel.Add(e);
                }
            }
            return lePersonnel;
        }
    }
}