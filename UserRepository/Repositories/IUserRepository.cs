using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRepository.Model;

namespace UserRepository.Repositories
{
    public interface IUserRepository : IRepository
    {
        //Task<IEnumerable<User>> GetList();
        Task<List<User>> GetListAsync();
        User Add(User user);
    }
}
