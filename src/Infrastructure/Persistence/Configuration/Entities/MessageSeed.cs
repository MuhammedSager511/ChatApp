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
                        SenderId = "3fdcae8b-cc55-4098-ad5d-48c8c466b0f1",
                        SenderUserName = "muhammed",
                        RecipientId = "7226023f-be7e-4b0b-b26c-5e2ca69e811f",
                        RecipientUserName = "Ali",
                        IsActive = true,
                    },
                    new Message()
                    {
                        Id = 2,
                        Content = "test-2",
                        SenderId = "9b42d11a-7fd9-4fc2-a391-22c8f9d1fce0",
                        SenderUserName = "Muath",
                        RecipientId = "7226023f-be7e-4b0b-b26c-5e2ca69e811f",
                        RecipientUserName = "Ali",
                        IsActive = true,
                    }
                   
                );
        }
    }
}
