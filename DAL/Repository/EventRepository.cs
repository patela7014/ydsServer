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
    public class EventRepository : IRepository<Event>
    {
        private readonly ApiContext context;

        public EventRepository(ApiContext context)
        {
            this.context = context;
        }

        public string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public void Add(Event entity)
        {
            context.Event.Add(entity);
        }

        public void Delete(Event entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Event entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Event> FindBy(Expression<Func<Event, bool>> predicate)
        {
            return context.Event
                .Include(e => e.Users)
                    .ThenInclude(a => a.User)
                .AsQueryable()
                .Where(predicate);
        }

        public IQueryable<Event> GetAll()
        {
            return this.context.Event
                .Include(e => e.Users)
                    .ThenInclude(a => a.User)
                .AsQueryable();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
