using DAL.Core.Models;
using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApiContext context;

        public UserRepository(ApiContext context)
        {
            this.context = context;
        }

        public ApiContext Context { get; }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(User entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> FindBy(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public IQueryable<User> GetAll()
        {
            return this.context.User
                .AsQueryable();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
