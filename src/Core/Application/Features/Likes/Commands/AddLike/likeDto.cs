using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Likes.Commands.AddLike
{
    public class likeDto
    {
        public string Id { get; set; } 
        public string? UserName { get; set; }
        public int Age { get; set; }
        public string? KnownAS { get; set; }
        public string? PhotoUrl { get; set; }
        public string? City { get; set; }

    }
}
