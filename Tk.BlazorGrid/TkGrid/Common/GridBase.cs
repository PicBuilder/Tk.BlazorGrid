using System;
using System.Collections.Generic;
using System.Text;

namespace TkGrid
{
	public class GridBase
	{
		
		//repository needed stuff
		public int TotalCount { get; set; }

		public int PageSize { get; set; } = 5;
		public string Search { get; set; }

		//public bool EnableSearch { get; set; } = false;

		//public bool EnableAdd { get; set; } = false;
		//public bool EnableFilter { get; set; } = false;

		public int MaxPages { get; set; } = 10;

		//frontend required stuff
		public int CurrentPage { get; set; }

		public int PagerTotalCount { get; set; }

		public int MinPage { get; set; }

		public int MaxPage { get; set; } = 0;
		//filter
		public string FilterColumnName { get; set; } = "Summary";
		//sort
		public string SortColumnName { get; set; } = "Id";
		public string SortDirection { get; set; } = "desc";

		//custom
		public GridBase ApplyGrid()
		{
			//EnableSearch = true;
			//EnableFilter = true;
			//EnableAdd = true;
			CurrentPage = PagerHelpers.GetCurrentPage(CurrentPage.ToString());
			PagerTotalCount = PagerHelpers.GetTotalPages(PageSize, TotalCount);
			MinPage = PagerHelpers.GetMinPageToRender(MaxPages, PagerTotalCount, CurrentPage);
			MaxPage = PagerHelpers.GetMaxPageToRender(MaxPages, PagerTotalCount, CurrentPage);

			return this;
		}
	}
}
