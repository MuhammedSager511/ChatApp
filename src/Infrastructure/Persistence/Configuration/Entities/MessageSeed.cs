using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Entities
{
    public class MessageSeed : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasData
                (
                    new Message()
                    {
                        Id = 1,
                        Content = "test-1",
                        SenderId = 11,
                        SenderUserName = "Muhammed",
                        RecipientId = 11,
                        RecipientUserName = "Ali",
                        IsActive = true,
                    },
                    new Message()
                    {
                        Id = 2,
                        Content = "test-2",
                        SenderId = 12,
                        SenderUserName = "Abdo",
                        RecipientId = 12,
                        RecipientUserName = "Ali",
                        IsActive = true,
                    },
                    new Message()
                    {
                        Id = 3,
                        Content = "test-3",
                        SenderId = 13,
                        SenderUserName = "Muhammed",
                        RecipientId = 13,
                        RecipientUserName = "Abdo",
                        IsActive = true,
                    }
                );
        }
    }
}
