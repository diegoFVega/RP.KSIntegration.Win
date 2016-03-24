using Engine.Enum;
using Engine.Operations.ResourcesOps;
using System;
using System.Data;
using System.Text;

namespace Engine.Operations.IntegrationsOps
{
	public class PurchaseOrder : IDisposable
	{
		private string _currentConnectionString;

		public string ShipDate { get; set; }

		public PurchaseOrder()
		{
			_currentConnectionString = string.Empty;
		}

		public PurchaseOrder(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~PurchaseOrder()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void IntegratePurchaseOrderInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};

			try
			{
				infoMessage.AppendLine("b. Integrando los Purchase Orders en staging area");
				if (dSet.Tables.Count > 0)
				{
					infoMessage.AppendLine("c. Procesando los encabezados de los Purchase Orders");
					engineDataHelper.GetQueryResult("KS_S_PO", dSet.Tables["purchaseOrders"]);

					infoMessage.AppendLine("d. Procesando los detalles de los Purchase Orders");
					engineDataHelper.GetQueryResult("KS_S_PODetail", dSet.Tables["details"]);

					if (dSet.Tables["breakdowns"] != null)
					{
						infoMessage.AppendLine("e. Procesando los breakdowns de los Purchase Orders");
						engineDataHelper.GetQueryResult("KS_S_PODetailBreakdown", dSet.Tables["breakdowns"]);
					}

					if (dSet.Tables["boxes"] != null)
					{
						infoMessage.AppendLine("f. Procesando los boxes de los Purchase Orders");
						engineDataHelper.GetQueryResult("KS_S_PODetailBox", dSet.Tables["boxes"]);
					}
				}
				infoMessage.AppendLine("g. Procesando por ETL las facturas");

				engineDataHelper.GetQueryResult(Queries.POStandarizeInfo, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
				engineDataHelper.GetQueryResult(Queries.POEtl, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Origen: {0}", ex.Source));
				stringBuilder.AppendLine(string.Format("Error: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Datos: {0}", ex.Data));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendLine(string.Format("Trace: {0}", ex.StackTrace));

				infoMessage.AppendLine(stringBuilder.ToString());

				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}
	}
}