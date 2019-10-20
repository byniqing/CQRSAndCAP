using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserRepository.Model
{
    public class SomeEvent : INotification
    {
        public SomeEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
