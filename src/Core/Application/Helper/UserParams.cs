using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class UserParams
    {
		private const int MaxPageSize = 150;
        public int PageNumber { get; set; } = 1;

		private int _pageSize=8;

		public int PageSize
		{
			get => _pageSize;
			set =>_pageSize=(value>MaxPageSize) ? MaxPageSize : value;
		}
		public string? CurrentUserName { get; set; }
		public string? Gender { get; set; }
		public int minAge { get; set; } = 10;
		public int maxAge { get; set; } = 100;

		public string OrderBy { get; set; } = "lastActive";

	}
}
