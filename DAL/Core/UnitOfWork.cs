using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext context;

        public UnitOfWork(ApiContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
