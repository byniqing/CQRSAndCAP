using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserRepository.Model;

namespace UserRepository.Events
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public class UserDomainEvent : INotification
    {
        public User User { get; }
        public string Tag { get; set; }

        public UserDomainEvent(User user,string tag)
        {
            User = user;
            Tag = tag;
        }
    }
}
