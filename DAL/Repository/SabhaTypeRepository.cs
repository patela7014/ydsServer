using DAL.Core.Models;
using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repository
{
    public class SabhaTypeRepository : IRepository<SabhaType>
    {
        private readonly ApiContext context;

        public SabhaTypeRepository(ApiContext context)
        {
            this.context = context;
        }

        public void Add(SabhaType entity)
        {
            context.SabhaType.Add(entity);
        }

        public void Delete(SabhaType entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(SabhaType entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SabhaType> FindBy(Expression<Func<SabhaType, bool>> predicate)
        {
            return context.SabhaType
                .AsQueryable()
                .Where(predicate);
        }

        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public IQueryable<SabhaType> GetAll()
        {
            return context.SabhaType
                .AsQueryable();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
