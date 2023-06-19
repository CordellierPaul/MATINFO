/***********************************************************************
 * Module:  IDonnee.cs
 * Author:  cordellp
 * Purpose: Definition of the Interface IDonnee
 ***********************************************************************/

using System.Collections.ObjectModel;

namespace MATINFO.Metier
{
    public interface ICrud<T>
    {
        /// <summary>
        /// Insère dans la base de données l'objet this (en SQL avec insert)
        /// </summary>
        void Create();

        /// <summary>
        /// Lecture de l'objet
        /// </summary>
        void Read();

        /// <summary>
        /// Mise à jour de l'objet où les données de l'objet vont dans la base de données (en SQL avec update)
        /// </summary>
        void Update();

        /// <summary>
        /// Supprime l'objet dans la base de données (en SQL avec delete)
        /// </summary>
        void Delete();

        /// <summary>
        /// Parcours tout les objets <T> de la base de données et les renvoie (en sql avec select)
        /// </summary>
        /// <returns>Une ObservableCollection avec toutes les objets de la base de données de type <T></returns>
        ObservableCollection<T> FindAll();
    }
}