using System;
using System.Collections.Generic;
using System.Text;

namespace TkGrid
{
	public class Pager
	{
		//repository needed stuff
		public int TotalCount { get; set; }

		public int PageSize { get; set; } = 5;

		public string Search { get; set; }

		public bool EnableSearch { get; set; } = false;

		public int MaxPages { get; set; } = 10;

		//frontend required stuff
		public int CurrentPage { get; set; }

		public int PagerTotalCount { get; set; }

		public int MinPage { get; set; }

		public int MaxPage { get; set; } = 0;

		//sort
		public string SortColumnName { get; set; } = "Id";
		public string SortDirection { get; set; } = "desc";
	}
}
