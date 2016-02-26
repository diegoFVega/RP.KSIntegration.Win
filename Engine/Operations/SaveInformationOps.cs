using Engine.Enum;
using Engine.Operations.ResourcesOps;
using Engine.Utilities;
using System;
using System.Data;
using System.Text;
using System.Linq;

namespace Engine.Operations
{
	public class SaveInformationOps : IDisposable
	{
		private string _currentConnectionString;

		public SaveInformationOps(string currentConnectionString)
		{
			_currentConnectionString = currentConnectionString;
		}

		~SaveInformationOps()
		{
			Dispose();
		}

		public void Dispose()
		{
			_currentConnectionString = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public void SaveBoxTypeInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
					{
						var parameters = row.ToQueryParameters("p_");
						engineDataHelper.GetQueryResult(Queries.BoxTypeUpdate, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
					}
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_boxtype", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void SaveCustomerInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
					{
						var parameters = row.ToQueryParameters("p_");
						engineDataHelper.GetQueryResult(Queries.CustomerUpdate, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
					}
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Customer", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void SaveVendorInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
					{
						var parameters = row.ToQueryParameters("p_");
						engineDataHelper.GetQueryResult(Queries.VendorUpdate, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
					}
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Vendor", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void SaveLocationInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count > 0)
				{
					foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
					{
						var parameters = row.ToQueryParameters("p_");
						engineDataHelper.GetQueryResult(Queries.LocationUpdate, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
					}
				}
				dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Location", CommandType.Text, EngineDataHelperMode.ResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void SaveProductInformation(ref DataSet dSet, ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				if (dSet.Tables.Count <= 0)
					return;
				foreach (DataRow row in dSet.Tables[0].Rows.OfType<DataRow>())
				{
					var parameters = row.ToQueryParameters("p_");
					engineDataHelper.GetQueryResult(Queries.ProductUpdate, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet, parameters);
					dSet = (DataSet)engineDataHelper.GetQueryResult("select * from ks_Product", CommandType.Text, EngineDataHelperMode.ResultSet);
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}

		public void CreateSodInformation(ref StringBuilder infoMessage)
		{
			var stringBuilder = new StringBuilder();
			var engineDataHelper = new EngineDataHelper
			{
				CurrentStringConnection = _currentConnectionString
			};
			try
			{
				engineDataHelper.GetQueryResult(Queries.SodGenerator, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
				engineDataHelper.GetQueryResult(Queries.SodEtl, CommandType.StoredProcedure, EngineDataHelperMode.NonResultSet);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Ha ocurrido una excepcion en la ejecucion de la consulta.");
				stringBuilder.AppendLine(string.Format("Mensaje: {0}", ex.Message));
				stringBuilder.AppendLine(string.Format("Ubicacion: {0}", ex.TargetSite));
				throw new Exception(stringBuilder.ToString());
			}
			finally
			{
				engineDataHelper.Dispose();
			}
		}
	}
}