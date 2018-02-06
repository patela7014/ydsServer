using DAL.Core.Models;
using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repository
{
    public class AttendanceRepository : IRepository<EventAttendance>
    {
        private readonly ApiContext context;

        public AttendanceRepository(ApiContext context)
        {
            this.context = context;
        }

        public void Add(EventAttendance entity)
        {
            context.EventAttendance.Add(entity);
        }

        public void Delete(EventAttendance entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(EventAttendance entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EventAttendance> FindBy(Expression<Func<EventAttendance, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public string GenerateID()
        {
            throw new NotImplementedException();
        }

        public IQueryable<EventAttendance> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
