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
                        Content="test-1",
                        SenderId=1,
                        SenderUserName="Muhammed",
                        RecipientId=1,
                        RecipientUserName="Ali",
                        IsActive=true,
                    },
                    new Message()
                    {
                        Id = 2,
                        Content = "test-2",
                        SenderId = 2,
                        SenderUserName = "Abdo",
                        RecipientId = 2,
                        RecipientUserName = "Ali",
                        IsActive = true,
                    },
                    new Message()
                    {
                         Id = 3,
                         Content = "test-3",
                         SenderId = 3,
                         SenderUserName = "Muhammed",
                         RecipientId = 3,
                         RecipientUserName = "Abdo",
                         IsActive = true,
                    }
                );
        }
    }
}
