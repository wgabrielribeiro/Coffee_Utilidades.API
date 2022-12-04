using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Utilidades.Core.Interface
{
    public interface IGeralPersist
    {
        void Add<T>(T entity) where T : class;
        //void Update<T>(T entity) where T : class;
        //void Delete<T>(T entity) where T : class;
        //void DeleteRanger<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
