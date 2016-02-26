using System.Data;
using System.IO;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Engine.Utilities
{
	public static class DataSetExtension
	{
		public static StringBuilder FormattingOutput(this DataSet dSet, string templateFilePath, XsltArgumentList parameters = null)
		{
			var answer = new StringBuilder();
			var textos = new StringWriter(answer);
			var datos = new XPathDocument(new StringReader(dSet.GetXml()));
			var xtr = new XslCompiledTransform();

			xtr.Load(templateFilePath);
			xtr.Transform(datos, parameters, textos);

			return answer;
		}
	}
}