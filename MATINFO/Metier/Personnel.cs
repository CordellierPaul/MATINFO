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
        public string Email { get; set; }

        public List<EstAttribue> LesAttributions { get; set; }

        public Personnel(int idPersonnel, string nom, string prenom, string eMail)
        {
            IDPersonnel = idPersonnel;
            Nom = nom;
            Prenom = prenom;
            Email = eMail;

            LesAttributions = new List<EstAttribue>();
        }

        public Personnel() : this(1, "", "", "") { }


        public void Create()
        {
            new AccesDonnees().SetData($"insert into personnel (nompersonnel, prenompersonnel, emailpersonnel) " +
                                       $"values ('{this.Nom}', '{this.Prenom}', '{this.Email}');");
        }
   
        public void Read()
        {
            // TODO: implement
        }
   
        public void Update()
        {
            new AccesDonnees().SetData($"update personnel set nompersonnel = '{this.Nom}', " +
                                       $"prenompersonnel = '{this.Prenom}', " +
                                       $"emailpersonnel = '{this.Email}' " +
                                       $"where idpersonnel = {this.IDPersonnel};");
        }
   
        public void Delete()
        {
            // Suppression récurisve des attribution
            foreach (EstAttribue uneAttribution in LesAttributions)
                uneAttribution.Delete();

            new AccesDonnees().SetData($"delete from personnel where idpersonnel = {IDPersonnel};");
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
    }
}