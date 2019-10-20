using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRepository.Model;

namespace UserRepository.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetListAsync();
        Message Add(Message message);
    }
}
