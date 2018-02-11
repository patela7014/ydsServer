using DAL.Core.Models;
using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repository
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly ApiContext context;

        public AddressRepository(ApiContext context)
        {
            this.context = context;
        }

        public void Add(Address entity)
        {
            context.Address.Add(entity);
        }

        public void Delete(Address entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Address entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Address> FindBy(Expression<Func<Address, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public IQueryable<Address> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
