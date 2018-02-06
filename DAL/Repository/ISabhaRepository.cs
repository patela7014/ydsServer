using DAL.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repository
{
    public interface ISabhaRepository : IRepository<Sabha>
    {
        IQueryable<Sabha> GetSabhaUsers(Expression<Func<Sabha, bool>> predicate);
    }
}
