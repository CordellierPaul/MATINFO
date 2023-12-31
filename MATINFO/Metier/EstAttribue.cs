using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Windows.Documents;

namespace MATINFO.Metier
{
    public class EstAttribue : ICrud<EstAttribue>
    {
        #region declaration de variables
        public int IDMateriel { get; set; }
        public Materiel? UnMateriel { get; set; }
        public int IDPersonnel { get; set; }
        public Personnel? UnPersonnel { get; set; }
        public string DateAttribution { get; set; }
        public string? Commentaire { get; set; }
        #endregion

        #region Constructeurs
        public EstAttribue(int idMateriel, int idPersonnel, string dateAttribution, string? commentaire)
        {
            IDMateriel = idMateriel;
            IDPersonnel = idPersonnel;
            DateAttribution = dateAttribution;
            Commentaire = commentaire;
        }

        public EstAttribue() : this(1, 1, DateTime.Today.ToShortDateString(), "") { }
        #endregion

        #region Implementation de l'interface CRUD
        public void Create()
        {
            if (string.IsNullOrWhiteSpace(Commentaire))
                new AccesDonnees().SetData($"insert into est_attribue (idmateriel, idpersonnel, dateattribution) " +
                                       $"values ({this.IDMateriel}, {this.IDPersonnel}, '{this.DateAttribution}')");
            else
                new AccesDonnees().SetData($"insert into est_attribue (idmateriel, idpersonnel, dateattribution, commentaireattribution) " +
                                       $"values ({this.IDMateriel}, {this.IDPersonnel}, '{this.DateAttribution}', '{this.Commentaire}')");
        }
   
        public void Read()
        {
            // TODO: implement
        }
   
        public void Update()
        {
            if (string.IsNullOrWhiteSpace(Commentaire))
                new AccesDonnees().SetData($"update est_attribue set dateattribution = '{this.DateAttribution}'" +
                    $"where idmateriel = {this.IDMateriel} and idpersonnel = {this.IDPersonnel};");
            else
                new AccesDonnees().SetData($"update est_attribue set commentaireattribution = '{this.Commentaire}'," +
                    $"dateattribution = '{this.DateAttribution}'" +
                    $"where idmateriel = {this.IDMateriel} and idpersonnel = {this.IDPersonnel};");
        }
   
        public void Delete()
        {
            new AccesDonnees().SetData($"delete from est_attribue where idmateriel = {this.IDMateriel} and idpersonnel = {this.IDPersonnel} ;");
        }

        public ObservableCollection<EstAttribue> FindAll()
        {
            ObservableCollection<EstAttribue> lesAttributions = new ObservableCollection<EstAttribue>();
            AccesDonnees accesBD = new AccesDonnees();
            string requete = "select idmateriel, idpersonnel, dateattribution, commentaireattribution from est_attribue ;";
            DataTable datas = accesBD.GetData(requete)!;
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    EstAttribue e = new EstAttribue(int.Parse(row["idmateriel"].ToString()!), int.Parse(row["idpersonnel"].ToString()!), DateTime.Parse(row["dateattribution"].ToString()!).ToShortDateString(), row["commentaireattribution"].ToString());
                    lesAttributions.Add(e);
                }
            }
            return lesAttributions;
        }
        #endregion
    }
}