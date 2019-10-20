using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserApplication.Users.Commands;
using UserRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using UserRepository.Model;

namespace UserApplication.Commands.Users
{
    /// <summary>
    /// 接收api发起的命令
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly UserRepository.Repositories.IUserRepository _userRepository;
        public CreateUserCommandHandler(UserRepository.Repositories.IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 命令处理者
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = _userRepository.Add(request.User);
            await _userRepository.UnitOfWork.SaveEntitiesAsync();
            return entity;
        }
    }
}
