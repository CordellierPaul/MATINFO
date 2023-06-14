/***********************************************************************
 * Module:  IDonnee.cs
 * Author:  cordellp
 * Purpose: Definition of the Interface IDonnee
 ***********************************************************************/

namespace MATINFO.Metier
{
    public interface ICrud
    {
        void Create();
        ICrud? Read();
        void Update();
        void Delete();

        //ObservableCollection<T> FindAll();
    }
}