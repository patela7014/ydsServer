using DAL.Core.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Sabha> SabhaRepository { get; }
        IRepository<Event> EventRepository { get; }
        Task CompleteAsync();
    }
}
