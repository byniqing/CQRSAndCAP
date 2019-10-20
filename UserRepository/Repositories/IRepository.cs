using System;
using System.Collections.Generic;
using System.Text;

namespace UserRepository.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
