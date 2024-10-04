using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Persistence.Configuration.Entities
{
    public class UserPhotoSeed : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasData(
                new Photo()
                {
                    Id = 1,
                    AppUserId = "d26d32da-d505-43b5-9e6d-51b667b27a0c",
                    IsActive = true,
                    IsMain = true,
                    Url = "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA",
                    PublicId = "some-unique-public-id2"
                },
                   new Photo()
                   {
                       Id=2,
                       AppUserId = "160465c2-b0b2-4fb7-abe1-56ac9944b894",
                       IsActive = true,
                       IsMain = false,
                       Url = "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA",
                       PublicId = "some-unique-public-id2"
                   },
                      new Photo()
                      {
                          Id=3,
                          AppUserId = "2ff4a611-8f2c-4540-ace1-0da73cb212e0",
                          IsActive = true,
                          IsMain = false,
                          Url = "https://www.google.com/imgres?q=user&imgurl=https%3A%2F%2Fcdn-icons-png.flaticon.com%2F512%2F219%2F219986.png&imgrefurl=https%3A%2F%2Fwww.flaticon.com%2Ffree-icon%2Fuser_219986&docid=_W1KK9rlaF0E_M&tbnid=J8FAsbNvH-TK-M&vet=12ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA..i&w=512&h=512&hcb=2&ved=2ahUKEwj2-sOw3vKIAxU6g_0HHWUHKowQM3oECGoQAA",
                          PublicId = "some-unique-public-id3"
                      }
                );
        }
    }
}
