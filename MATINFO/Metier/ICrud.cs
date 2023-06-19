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
        /// Ins�re dans la base de donn�es l'objet this (en SQL avec insert)
        /// </summary>
        void Create();

        /// <summary>
        /// Lecture de l'objet
        /// </summary>
        void Read();

        /// <summary>
        /// Mise � jour de l'objet o� les donn�es de l'objet vont dans la base de donn�es (en SQL avec update)
        /// </summary>
        void Update();

        /// <summary>
        /// Supprime l'objet dans la base de donn�es (en SQL avec delete)
        /// </summary>
        void Delete();

        /// <summary>
        /// Parcours tout les objets <T> de la base de donn�es et les renvoie (en sql avec select)
        /// </summary>
        /// <returns>Une ObservableCollection avec toutes les objets de la base de donn�es de type <T></returns>
        ObservableCollection<T> FindAll();
    }
}