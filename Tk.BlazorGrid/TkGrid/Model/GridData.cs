using System;
using System.Collections.Generic;
using System.Text;

namespace TkGrid
{
    public class GridData<T>
    {
		public GridData()
		{
			Data = new List<T>();
		}

		public int PageSize { get; set; }

		public int TotalCount { get; set; }

		public List<T> Data { get; set; }
	}
}
