using System.Collections.Generic;
using System.Data;

namespace Engine.Utilities
{
	internal class DataColumnComparer : IEqualityComparer<DataColumn>
	{
		public static DataColumnComparer ColumnInstance { get; set; }

		private DataColumnComparer()
		{
		}

		public bool Equals(DataColumn origin, DataColumn target)
		{
			if (origin.ColumnName != target.ColumnName)
				return false;
			if (origin.DataType != target.DataType)
				return false;

			return true;
		}

		public int GetHashCode(DataColumn column)
		{
			var hash = 17;
			hash = 31 * hash + column.ColumnName.GetHashCode();
			hash = 31 * hash + column.DataType.GetHashCode();

			return hash;
		}
	}
}