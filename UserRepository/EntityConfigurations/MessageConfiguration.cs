using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UserRepository.Model;

namespace UserRepository.EntityConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> messageConfiguration)
        {
            messageConfiguration
                .ToTable("message")
                .HasKey(m => m.id);

        }
    }
}
