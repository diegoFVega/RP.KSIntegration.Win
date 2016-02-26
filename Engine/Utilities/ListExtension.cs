using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Engine.Utilities
{
	public static class ListExtension
	{
		public static string ArrayToString(this Array items)
		{
			var builder = new StringBuilder();
			foreach (var obj2 in items)
			{
				builder.AppendLine(obj2.ToString());
			}
			return builder.ToString();
		}

		public static DataTable ToDataTable<T>(this List<T> items)
		{
			var table = new DataTable(typeof(T).Name);
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var info in properties)
			{
				table.Columns.Add(info.Name);
			}
			foreach (var local in items)
			{
				var values = new object[properties.Length];
				for (var i = 0; i < properties.Length; i++)
				{
					values[i] = properties[i].GetValue(local, null);
				}
				table.Rows.Add(values);
			}
			return table;
		}
	}
}