using System.Data;
using System.Linq;

namespace Engine.Utilities
{
	public static class DataTableExtension
	{
		public static bool SchemaEquals(this DataTable table, DataTable anotherTable)
		{
			if (table.Columns.Count != anotherTable.Columns.Count)
			{
				return false;
			}

			var first = table.Columns.Cast<DataColumn>();
			var second = anotherTable.Columns.Cast<DataColumn>();

			return (first.Except(second, DataColumnComparer.ColumnInstance).Any());
		}
	}
}