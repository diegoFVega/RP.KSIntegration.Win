using Engine.Enum;
using Engine.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Engine
{
	public class EngineOperationHelper : IDisposable
	{
		public EngineOperationHelper()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
		}

		~EngineOperationHelper()
		{
			Dispose();
		}

		public void Dispose()
		{
			GC.Collect();
			GC.SuppressFinalize(this);
		}

		public static T GetObjectFromJson<T>(string url, string requestContentType, HttpVerb requestMethod, SortedList<string, string> parameters = null)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

			var stringBuilder = new StringBuilder();
			T obj;

			try
			{
				var webRequest = WebRequest.Create(url.FixJsonUrl(parameters));
				var response = webRequest.GetResponse();
				webRequest.ContentType = requestContentType;
				webRequest.Method = requestMethod.ToString();
				webRequest.Timeout = 50000; //se agrega tiempo de espera para matar la conexion de la descarga (solicitado por Komet)

				obj = JsonConvert.DeserializeObject<T>(response.GetResponseStream().GetStringData());
			}
			catch (JsonSerializationException ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion general.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion general.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return obj;
		}

		public static DataSet GetTableFromJson<T>(string url, string requestContentType, HttpVerb requestMethod, string nodeName, SortedList<string, string> parameters = null)
		{
			var stringBuilder = new StringBuilder();
			DataSet dataSet;
			try
			{
				ServicePointManager.Expect100Continue = true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

				var webRequest = WebRequest.Create(url.FixJsonUrl(parameters));

				webRequest.ContentType = requestContentType;
				webRequest.Method = requestMethod.ToString();
				webRequest.Timeout = 60000; //se agrega tiempo de espera para matar la conexion de la descarga (solicitado por Komet)

				var nodeList = JsonConvert.DeserializeXNode(webRequest.GetResponse().GetResponseStream().GetStringData(), typeof(T).Name).ToXmlDocument().SelectNodes(string.Format("{0}/{1}", typeof(T).Name, nodeName));
				var xmlDocument = new XmlDocument();

				xmlDocument.LoadXml(nodeList.FromNodesToString(typeof(T).Name));
				dataSet = xmlDocument.ToDataSet();
			}
			catch (XmlException ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion al manejar el objeto XmlDocument.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			catch (DataException ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion al crear el set de datos.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			catch (JsonSerializationException ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion al obtener el objeto JSON.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion general.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return dataSet;
		}

		public static T GetObjectFromJson<T>(string url, string requestContentType, string requestMethod, JObject parameters = null) where T : new()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

			var stringBuilder = new StringBuilder();
			var obj = default(T);

			try
			{
				var webRequest = (HttpWebRequest)WebRequest.Create(url);
				webRequest.ContentType = string.Format("{0}; {1}", requestContentType, "charset=utf-8");
				webRequest.Method = requestMethod;
				webRequest.Timeout = 50000; //se agrega tiempo de espera para matar la conexion de la descarga (solicitado por Komet)

				using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
				{
					streamWriter.Write(JsonConvert.SerializeObject(parameters, Formatting.None));
					streamWriter.Flush();
				}
				var responseStream = webRequest.GetResponse().GetResponseStream();
				if (responseStream != null)
				{
					using (var streamReader = new StreamReader(responseStream))
						obj = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
				}
			}
			catch (JsonSerializationException ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion general.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion general.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			return obj;
		}
	}
}