using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        //CRUD
        //Type Name();
        List<T> List();
        void Insert(T par);
        T Get(Expression<Func<T, bool>> filter);
        void Update(T par);
        void Delete(T par);
        List<T> List(Expression<Func<T, bool>> filter);
    }
}
