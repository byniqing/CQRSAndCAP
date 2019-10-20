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
�����ռ���������������������������Դ�����Ŀ��ò�Ҫ���� 
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
             MediatR.Extensions.Microsoft.DependencyInjection ����������MediatR
             ����Ҫע��:
             ����������UserDomainEventHandler��û�ڵ�ǰ�������棬�����ֶ�ָ��
             */
            services.AddMediatR(typeof(Startup),typeof(UserDomainEventHandler));
            //services.AddSingleton<UserContext>();

            var connectionString = Configuration.GetConnectionString("sql");
            var migrationAssemble = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<UserContext>(options =>
            {
                //��UserContext�ڵ�ǰ��Ŀ������ע�뼴��
                //options.UseSqlServer(connectionString);


                options.UseSqlServer(connectionString, sql =>
                {
                    /*
                    ��UserContext�����Ĳ��ڵ�ǰ��Ŀ��ʱ��Ҫ����ע��
                    �����ڵ�ǰ�������������ģ��̳���DbContext������������ģ�
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
           //     //��ʽA
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
