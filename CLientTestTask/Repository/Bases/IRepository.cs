using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLientTestTask.Repository.Bases
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetValues();

        T GetValue(int id);

        string Update(int id, T value);

        string Delete(int id);

        T CreateValue(T value);
    }
}
