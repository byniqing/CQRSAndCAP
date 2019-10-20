using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UserRepository.Events;
using UserRepository.Model;

namespace UserApplication.DomainEventsHandlers
{
    /// <summary>
    /// 领域事件处理者
    /// 接收到了领域事件，可以做自己的业务
    /// 比如：也可以转成集成事件，通过CAP。发布消息到RabbitMq
    /// 其他微服务订阅即可
    /// 
    /// 集成事件的目的是将提交的事务和更新传播到其他子系统，无论它们是其他微服务，集成事件始终是异步的
    /// </summary>
    public class UserDomainEventHandler : INotificationHandler<UserDomainEvent>
    {
        private readonly ILogger<UserDomainEventHandler> _logger;

        public UserDomainEventHandler(ILogger<UserDomainEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(UserDomainEvent notification, CancellationToken cancellationToken)
        {
            //接收到了领域事件，转成集成事件

            /*
             集成事件，通过CAP操作，依赖NuGet包
             CAP是：基于最终一致性的微服务分布式事务解决方案，也是一种具有发件箱模式的事件总线。
             DotNetCore.CAP
             DotNetCore.CAP.RabbitMQ
             DotNetCore.CAP.SqlServer
             */


            _logger.LogWarning($"Handled: {notification.Tag}");
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }

    //public class UserDomainEventHandler : INotificationHandler<SomeEvent>
    //{
    //    //public UserDomainEventHandler()
    //    //{
    //    //}

    //    public Task Handle(SomeEvent notification, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
