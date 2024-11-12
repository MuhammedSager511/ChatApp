using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class LikeParams:PaginationParams
    {
        public string? UserId { get; set; }
        public string? Pridicate { get; set; }
    }
}
