using System;
using System.Collections.Generic;
using System.Text;

namespace TkGrid
{
    public class GridData<T> where T : class
	{
		public GridData()
		{
			Data = new List<T>();
		}

		public List<T> Data { get; set; }

		public int TotalCount { get; set; }

		public int PageSize { get; set; }		
	}
}
