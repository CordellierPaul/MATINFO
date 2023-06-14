using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MATINFO.Metier
{
    public class Personnel : ICrud<Personnel>
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

            LesAttributions = new List<EstAttribue>();
        }

        public Personnel() : this(1, "", "", "") { }

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
            // Suppression récurisve des attribution
            foreach (EstAttribue uneAttribution in LesAttributions)
                uneAttribution.Delete();

            LesAttributions.Clear();

            AccesDonnees accesBD = new AccesDonnees();
            string requete = "delete from personnel where idpersonnel = " + IDPersonnel + " ;";
            accesBD.SetData(requete);
        }

        public ObservableCollection<Personnel> FindAll()
        {
            ObservableCollection<Personnel> lePersonnel = new ObservableCollection<Personnel>();
            AccesDonnees accesBD = new AccesDonnees();
            string requete = "select idpersonnel, nompersonnel, prenompersonnel, emailpersonnel from personnel;";
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
        #endregion
    }
}