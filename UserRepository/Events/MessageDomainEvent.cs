using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserRepository.Model;

namespace UserRepository.Events
{
    public class MessageDomainEvent : INotification
    {
        private Message Message { get; }
        public MessageDomainEvent(Message message)
        {
            Message = message;
        }
    }
}
