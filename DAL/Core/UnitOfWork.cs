using DAL.Core.Models;
using DAL.Persistence;
using DAL.Repository;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext context;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Sabha> sabhaRepository;

        public UnitOfWork(ApiContext context, IRepository<User> userRepository,IRepository<Event> eventRepository, IRepository<Sabha> sabhaRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
            this.eventRepository = eventRepository;
            this.sabhaRepository = sabhaRepository;
        }

        public IRepository<User> UserRepository => userRepository;

        public IRepository<Sabha> SabhaRepository => sabhaRepository;

        public IRepository<Event> EventRepository => eventRepository;

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
