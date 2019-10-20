using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserRepository;
/*
命名空间跟类名有重名，必须这样，所以创建项目最好不要重名 
*/
using ur = UserRepository.Repositories;
using UserRepository.Repositories;
using UserApplication.Queries;
using UserApi.Extensions;
using UserApplication.Users.Commands;
using UserApplication.Commands.Users;
using UserApplication.DomainEventsHandlers;

namespace UserApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             MediatR.Extensions.Microsoft.DependencyInjection 包，包含了MediatR
             这里要注入:
             如果处理程序（UserDomainEventHandler）没在当前工程里面，必须手动指定
             */
            services.AddMediatR(typeof(Startup),typeof(UserDomainEventHandler));
            //services.AddSingleton<UserContext>();

            var connectionString = Configuration.GetConnectionString("sql");
            var migrationAssemble = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<UserContext>(options =>
            {
                //当UserContext在当前项目，这样注入即可
                //options.UseSqlServer(connectionString);


                options.UseSqlServer(connectionString, sql =>
                {
                    /*
                    当UserContext上下文不在当前项目的时候，要这样注入
                    即：在当前程序集中找上下文（继承了DbContext的类就是上下文）
                    */
                    sql.MigrationsAssembly(migrationAssemble);
                });
            }).Load();

            //services.Load();


            services.AddSingleton<IUserQueries, UserQueries>(options =>
             {
                 return new UserQueries(connectionString);
             });

            //services.AddSingleton<IUserRepository, ur.UserRepository>();

            //var provider = services.BuildServiceProvider();

            //var configuration = provider.GetService<IConfiguration>();

           // services.AddSingleton<IUserRepository, ur.UserRepository>(options =>
           //{
           //    var ck = services.BuildServiceProvider().GetService<UserContext>();
           //    var userContext = options.GetRequiredService<UserContext>();
           //    return new ur.UserRepository(ck);
           //});

           // services.AddSingleton<IRequestHandler<CreateUserCommand, User>, CreateUserCommandHandler>(options =>
           // {
           //     //方式A
           //     var ccc = options.GetRequiredService<IUserRepository>();
           //     return new CreateUserCommandHandler(ccc);
           // });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
