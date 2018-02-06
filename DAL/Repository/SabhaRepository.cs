using DAL.Core.Models;
using DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repository
{
    public class SabhaRepository : ISabhaRepository
    {
        private readonly ApiContext context;

        public SabhaRepository(ApiContext context)
        {
            this.context = context;
        }

        public void Add(Sabha entity)
        {
            context.Sabha.Add(entity);
        }

        public void Delete(Sabha entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Sabha entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sabha> FindBy(Expression<Func<Sabha, bool>> predicate)
        {
            return context.Sabha
                .Include(s => s.SabhaType)
                .AsQueryable()
                .Where(predicate);
        }

        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public IQueryable<Sabha> GetAll()
        {
            return context.Sabha
                .Include(s => s.Users)
                    .ThenInclude(su => su.User)
                .Include(s => s.SabhaType)
                .AsQueryable();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sabha> GetSabhaUsers(Expression<Func<Sabha, bool>> predicate)
        {
            return context.Sabha
                .Include(s => s.Users)
                    .ThenInclude(su => su.User)
                .Include(s => s.SabhaType)
                .AsQueryable()
                .Where(predicate);
        }
    }
}
