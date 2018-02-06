using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
