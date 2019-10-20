using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserRepository.Model;

namespace UserRepository.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly UserContext _context;
        public MessageRepository(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Message Add(Message message)
        {
            return _context.Add(message).Entity;
        }

        public async Task<IEnumerable<Message>> GetListAsync()
        {
            return await _context.message.ToListAsync();
        }
    }
}
