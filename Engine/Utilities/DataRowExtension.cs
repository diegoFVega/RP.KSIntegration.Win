using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Engine.Utilities
{
	public static class DataRowExtension
	{
		public static SortedList<string, string> ToQueryParameters(this DataRow row, string preffix = null, string suffix = null)
		{
			var list = new SortedList<string, string>();
			var columns = row.Table.Columns;
			list.Clear();
			foreach (DataColumn column in columns.OfType<DataColumn>())
			{
				list.Add(string.Format("{0}{1}{2}", string.IsNullOrEmpty(preffix) ? string.Empty : preffix, column.ColumnName, string.IsNullOrEmpty(suffix) ? string.Empty : suffix), string.IsNullOrEmpty(row[column.ColumnName].ToString()) ? string.Empty : row[column.ColumnName].ToString());
			}
			return list;
		}
	}
}