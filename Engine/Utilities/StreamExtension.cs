using System.IO;

namespace Engine.Utilities
{
	public static class StreamExtension
	{
		public static string GetStringData(this Stream data)
		{
			var reader = new StreamReader(data);
			return reader.ReadToEnd();
		}
	}
}