using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserApplication.Commands.Users;
using UserApplication.DomainEventsHandlers;
using UserApplication.Queries;
using UserApplication.Users.Commands;
using UserRepository;
using UserRepository.Events;
using UserRepository.Model;
using UserRepository.Repositories;
using ur = UserRepository.Repositories;

namespace UserApi.Extensions
{
    /// <summary>
    /// 把依赖注入抽取放在这里，统一管理
    /// </summary>
    public static class RegisterInjection
    {
        public static IServiceCollection Load(this IServiceCollection services)
        {
            /*
            AddTransient 每次service请求都是获得不同的实例
            AddScoped 对于同一个请求返回同一个实例，不同的请求返回不同的实例
            AddSingleton 每次都是获得同一个实例
            */
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            var userContext = provider.GetService<UserContext>();
            var connectionString = configuration.GetConnectionString("sql");

            //services.AddSingleton<ur.UserRepository>();

            services.AddSingleton<IUserQueries, UserQueries>(options =>
            {
                return new UserQueries(connectionString);
            });
            //services.AddSingleton<IUserRepository, ur.UserRepository>();

            services.AddSingleton<IUserRepository, ur.UserRepository>(options =>
             {
                 //这样会异常
                 //var userContext = options.GetRequiredService<UserContext>();
                 return new ur.UserRepository(userContext);
             });

            #region 将命令模型和命令处理程序匹配
            services.AddScoped<IRequestHandler<CreateUserCommand, User>, CreateUserCommandHandler>(options =>
            {
                var _userRepository = options.GetRequiredService<IUserRepository>();
                return new CreateUserCommandHandler(_userRepository);
            });
            #endregion

            //services.AddScoped<INotificationHandler<UserDomainEvent>, UserDomainEventHandler>();
            
            return services;
        }
    }
}
