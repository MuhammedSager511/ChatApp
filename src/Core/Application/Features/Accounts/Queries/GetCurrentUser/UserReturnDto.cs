﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Queries.GetCurrentUser
{
    public class UserReturnDto
    {
        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
