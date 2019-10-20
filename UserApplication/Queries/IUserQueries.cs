using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRepository.Model;

namespace UserApplication.Queries
{
    public interface IUserQueries
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
