using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using UserRepository.Model;

namespace UserRepository.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userConfiguration)
        {
            /*
             * 如果没有强调
             值类型默认是不能为空
             引用类型可空
             */

            //Microsoft.EntityFrameworkCore
            //Microsoft.EntityFrameworkCore.Relational
            userConfiguration.ToTable("users", "usering") //schema 是表架构
                .HasKey(u => u.Id);

            /*
             注意SqlDbType和DbType的区别
             */
            var a = nameof(SqlDbType.NVarChar);
            var b = nameof(DbType.String);

            userConfiguration
                .Property(u => u.name)
                //.HasMaxLength(50) 这样映射到数据库，变成了nvarchar(1)
                .HasColumnType($"{nameof(SqlDbType.NVarChar)}(100)");

            //
            userConfiguration
                .Property(u => u.createTime)
                .HasDefaultValueSql("GetDate()");

            /*
             name强调不能为空
             address没强调，默认是可空
             */
            userConfiguration.Property(u => u.name).IsRequired();
        }
    }
}
