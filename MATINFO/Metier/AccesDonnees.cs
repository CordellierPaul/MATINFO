// File:    DataAccess.cs
// Author:  nmege
// Created: samedi 26 novembre 2022 11:00:28
// Purpose: Definition of Class DataAccess

using System;
using System.Data;
using System.Windows;
using Npgsql;

//ATTENTION à bien donner les droits à votre utilisateur :
//GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO matinfo;
//GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO matinfo;

namespace MATINFO.Metier
{
    public class AccesDonnees
    {
        public NpgsqlConnection? NpgSQLConnect { get; set; }

        #region Gestion de la connection a la base
        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        /// <returns> Retourne un booléen indiquant si la connexion est ouverte ou fermée</returns>
        public bool OpenConnection()
        {
            try
            {
                // Site pour la modification de la base de données : https://srv-peda.iut-acy.local/phppgadmin/

                NpgSQLConnect = new NpgsqlConnection
                {
                    ConnectionString = "Server=srv-peda-new;port=5433;Database=projets_wpf;Search Path=bd_matinfo;uid=cordellp;password=HADJjx;"
                };
                NpgSQLConnect.Open();

                return NpgSQLConnect.State.Equals(ConnectionState.Open);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Déconnexion de la base de données
        /// </summary>
        private void CloseConnection()
        {
            if (NpgSQLConnect != null)
                if (NpgSQLConnect.State.Equals(ConnectionState.Open))
                {
                    NpgSQLConnect.Close();
                }
        }
        #endregion

        #region gestion des interactions avec la base
        /// <summary>
        /// Accès à des données en lecture
        /// </summary>
        /// <returns>Retourne un DataTable contenant les lignes renvoyées par le SELECT</returns>
        /// <param name="getQuery">Requête SELECT de sélection de données</param>
        public DataTable? GetData(string getQuery)
        {
            try
            {
                if (OpenConnection())
                {
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(getQuery, NpgSQLConnect);
                    /*NpgsqlDataAdapter npgsqlAdapter = new NpgsqlDataAdapter
                    {
                        SelectCommand = npgsqlCommand
                    };*/
                    NpgsqlDataAdapter npgsqlAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                    DataTable dataTable = new DataTable();
                    npgsqlAdapter.Fill(dataTable);
                    CloseConnection();

                    return dataTable;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                CloseConnection();
                return null;
            }
        }

        /// <summary>
        /// Permet d'insérer, supprimer ou modifier des données
        /// </summary>
        /// <returns>Retourne un entier contenant le nombre de lignes ajoutées/supprimées/modifiées</returns>
        /// <param name="setQuery">Requête SQL permettant d'insérer (INSERT), supprimer (DELETE) ou modifier des données (UPDATE)</param>
        public int SetData(string setQuery)
        {
            try
            {
                if (OpenConnection())
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand(setQuery, NpgSQLConnect);
                    int modifiedLines = sqlCommand.ExecuteNonQuery();
                    CloseConnection();
                    return modifiedLines;
                }
                else
                    return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                CloseConnection();
                return 0;
            }
        }
        #endregion

        //public string CheckForQuotes(string text)
        //{
        //    if (text.Contains("'") && !text.Contains("''"))
        //        return $"{text.Substring(0, text.IndexOf("'"))}'{text.Substring(text.IndexOf("'")-1)}";
        //    else
        //        return text;
        //}
    }
}