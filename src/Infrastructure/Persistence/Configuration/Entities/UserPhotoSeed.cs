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
                    AppUserId = "23389a67-db38-448d-9c16-b6769eed9a01",
                    IsActive = true,
                    IsMain = true,
                    Url = "https://xsgames.co/randomusers/assets/avatars/male/31.jpg",
                    PublicId = "some-unique-public-id55"
                },
                   new Photo()
                   {
                       Id = 2,
                       AppUserId = "23389a67-db38-448d-9c16-b6769eed9a01",
                       IsActive = true,
                       IsMain = true,
                       Url = "https://xsgames.co/randomusers/assets/avatars/male/41.jpg",
                       PublicId = "some-unique-public-id5533"
                   },
                      new Photo()
                      {
                          Id = 3,
                          AppUserId = "23389a67-db38-448d-9c16-b6769eed9a01",
                          IsActive = true,
                          IsMain = true,
                          Url = "https://xsgames.co/randomusers/assets/avatars/male/21.jpg",
                          PublicId = "some-unique-public-id553"
                      }
                );
        }
    }
}
