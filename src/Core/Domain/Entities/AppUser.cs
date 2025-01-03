﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class AppUser:IdentityUser
    {
        public AppUser()
        {
            Photos = new HashSet<Photo>();
            LikedByUser = new HashSet<UserLike>();
            LikeUser = new HashSet<UserLike>();
            MessageSend = new HashSet<Message>();
            MessageRecived = new HashSet<Message>();
        }
        
        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastActive {get; set;} = DateTime.Now;

        public string Gender { get; set; } = string.Empty;

        public string Introduction {get; set; } = string.Empty;

        public string LookingFor { get; set; } = string.Empty;

        public string Interests { get; set; } = string.Empty;

        public string City{get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;


        public ICollection<Photo> Photos { get; set; }
        //like
        public ICollection<UserLike> LikeUser { get; set; }
        public ICollection<UserLike> LikedByUser { get; set; }
        //message
        public ICollection<Message> MessageSend { get; set; }
        public ICollection<Message> MessageRecived { get; set; }
    }
}
