using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Engine.Utilities
{
	public static class DocumentExtension
	{
		public static string FromNodesToString(this XmlNodeList nodeList, string root)
		{
			var seed = string.Empty;
			if (nodeList != null)
			{
				seed = nodeList.Cast<XmlNode>().Aggregate<XmlNode, string>(seed, (current, node) => current + node.OuterXml);
			}
			return string.Format("<{0}>{1}</{0}>", root, seed);
		}

		public static DataSet ToDataSet(this XmlDocument xmlDocument)
		{
			var set = new DataSet();
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlDocument.OuterXml.Trim()));
			set.ReadXml(stream);
			return set;
		}

		public static XDocument ToXDocument(this XmlDocument xmlDocument)
		{
			using (var reader = new XmlNodeReader(xmlDocument))
			{
				reader.MoveToContent();
				return XDocument.Load(reader);
			}
		}

		public static XmlDocument ToXmlDocument(this XDocument xDocument)
		{
			var document = new XmlDocument();
			using (var reader = xDocument.CreateReader())
			{
				document.Load(reader);
			}
			return document;
		}
	}
}