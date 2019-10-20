using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserRepository.EntityConfigurations;
using UserRepository.Events;
using UserRepository.Model;
using UserRepository.Repositories;

namespace UserRepository
{
    public class UserContext : DbContext, IUnitOfWork
    {
        /*
        这里不用实现IUniOfWork中的SaveChangesAsync方法
          是因为SaveChangesAsync跟DbContext中的SaveChangesAsync重名
        */
        public DbSet<User> users { get; set; }
        public DbSet<Message> message { get; set; }
        private readonly IMediator _mediator;
        public UserContext(DbContextOptions<UserContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            //await _mediator.Publish(new UserDomainEvent(new User { name="hh"},"ces ce "));
            //await _mediator.Publish(new SomeEvent("Hello World767"));

            //添加事物，但没有执行调度
            await _mediator.DispatchDomainEventsAsync(this);

            //SaveChangesAsync的时候，会一致性，因为如果上面失败了。这里也就可以不用执行了
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
