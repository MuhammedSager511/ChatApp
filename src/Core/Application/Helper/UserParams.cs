using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class UserParams:PaginationParams
    {
		
		public string? CurrentUserName { get; set; }
		public string? Gender { get; set; }
		public int minAge { get; set; } = 10;
		public int maxAge { get; set; } = 100;

		public string OrderBy { get; set; } = "lastActive";

	}
}
