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
        void Create();
        void Read();
        void Update();
        void Delete();

        ObservableCollection<T> FindAll();
    }
}