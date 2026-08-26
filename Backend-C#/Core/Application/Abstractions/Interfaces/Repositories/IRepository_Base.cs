using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Interfaces.Repositories
{
    public interface IRepository_Base <T> 
    {
        public void create(T entity);
        public List<T> read();
        public void update(T entity);
        public int delete(T entity);

    }
}
