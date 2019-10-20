using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserRepository.Model;

namespace UserRepository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public User Add(User user)
        {
            /*
             * 测试：添加一个事件，告诉message，有新增
             */
            user.AddCreateUserDomainEvent();
            return _context.Add(user).Entity;
        }

        public async Task<List<User>> GetListAsync()
        {
            return await _context.users.ToListAsync();
        }
    }
}
