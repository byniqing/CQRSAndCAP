using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Queries;
using UserApplication.Users.Commands;
using MediatR;
using UserRepository.Model;
using Microsoft.Extensions.Logging;
using System.Threading;
using UserRepository.Events;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserQueries _userQueries;
        private readonly IMediator _mediator;
        public UserController(IUserQueries userQueries, IMediator mediator)
        {
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var user = await _userQueries.GetAllAsync();
            return Ok(user);
        }

        /// <summary>
        /// 发送一个创建命令
        /// </summary>
        /// <param name="createUserCommand"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser([FromBody]User createUserCommand)
        {
            var user = new User() { 
            
            };
            return  await _mediator.Send(new CreateUserCommand(createUserCommand));
            //return await _mediator.Send(createUserCommand);
        }
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            await _mediator.Publish(new SomeEvent("Hello World"));
            return Ok();
        }
    }
    //public class SomeEvent : INotification
    //{
    //    public SomeEvent(string message)
    //    {
    //        Message = message;
    //    }

    //    public string Message { get; }
    //}

    public class Handler1 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler1> _logger;

        public Handler1(ILogger<Handler1> logger)
        {
            _logger = logger;
        }
        public Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handled: {notification.Message}");
            return Task.CompletedTask;
        }
    }
    public class Handler2 : INotificationHandler<UserDomainEvent>
    {
        private readonly ILogger<Handler2> _logger;

        public Handler2(ILogger<Handler2> logger)
        {
            _logger = logger;
        }
        public Task Handle(UserDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handled: {notification.Tag}");
            var a = notification.User;
            return Task.CompletedTask;
        }
    }
}