using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MediatR;
using UserRepository.Model;

namespace UserApplication.Users.Commands
{
    [DataContract]
    public class CreateUserCommand : IRequest<User>
    {
        public string ab { get; set; }
        public CreateUserCommand()
        {

        }
        public User User { get; private set; }
        public CreateUserCommand(User user)
        {
            User = user;
        }
    }
}
