using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using DataType.Purchase;
using Engine.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
	public class EngineQueueHelper
	{
		public string AccessKey { get; set; }
		public string SecretKey { get; set; }
		public string AmazonQueueAddress { get; set; }
		public RegionEndpoint RegionEndpointPlace { get; set; }

		private AmazonSQSClient _client;

		public EngineQueueHelper()
		{
			AccessKey = string.Empty;
			SecretKey = string.Empty;
			AmazonQueueAddress = string.Empty;
			RegionEndpointPlace = null;
		}

		private void OpenQsqClient()
		{
			var basicCredential = new BasicAWSCredentials(AccessKey, SecretKey);
			_client = new AmazonSQSClient(basicCredential, RegionEndpointPlace);
		}

		private string GetQueueUrl()
		{
			var url = string.Empty;

			if (
				!AmazonQueueAddress.IsAValidString(
					@"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"))
			{
				var request = new GetQueueUrlRequest
				{
					QueueName = AmazonQueueAddress,
					QueueOwnerAWSAccountId = "905040198233"
				};

				var response = _client.GetQueueUrl(request);
				url = response.QueueUrl;
			}
			else
			{
				url = AmazonQueueAddress;
			}
			return url;
		}

		public List<ProcessablePO> QueueProcess(ref StringBuilder infoMessage)
		{
			var poNumberList = new List<ProcessablePO>();

			try
			{
				var receiveMessageRequest = new ReceiveMessageRequest { QueueUrl = GetQueueUrl() };
				var receiveMessageResponse = new ReceiveMessageResponse();
				do
				{
					//Receiving a message
					OpenQsqClient();
					receiveMessageResponse = _client.ReceiveMessage(receiveMessageRequest);

					if (receiveMessageResponse.Messages.Count != 0)
					{
						var data = JsonConvert.DeserializeObject<ProcessablePO>(receiveMessageResponse.Messages[0].Body);
						var messageRecieptHandle = receiveMessageResponse.Messages[0].ReceiptHandle;

						//Deleting a message
						var deleteRequest = new DeleteMessageRequest
						{
							QueueUrl = GetQueueUrl(),
							ReceiptHandle = messageRecieptHandle
						};

						poNumberList.Add(data);
						_client.DeleteMessage(deleteRequest);
					}
				}
				while (receiveMessageResponse.Messages.Count != 0);
			}
			catch (AmazonSQSException ex)
			{
				infoMessage.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				infoMessage.AppendLine(string.Format("Origen: {0}", ex.Source));
				infoMessage.AppendLine(string.Format("Error: {0}", ex.Message));
				infoMessage.AppendLine(string.Format("Datos: {0}", ex.Data));
				infoMessage.AppendLine(string.Format("Trace: {0}", ex.StackTrace));
				throw new Exception(infoMessage.ToString());
			}

			return poNumberList;
		}
	}
}