using System;
using System.Runtime.InteropServices;

namespace BusinessObjects
{
	[Guid("BFAFC67A-0F7A-457B-9325-48A44CCE05DF"),
		 InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IDownloadTaskforce
	{
		[DispId(1)]
		string TestCreateObject(string testString = null);

		[DispId(2)]
		void ExecuteProcess(string processToExecute);

		[DispId(3)]
		string DownloadInvoiceInformation(string invoiceDate, string invoiceStatus, string location);

		[DispId(4)]
		string DownloadStandingOrderInformation(string startSoDate, string endSoDate);

		[DispId(5)]
		string DownloadPurchaseOrderInformarion(string shipDate);

		[DispId(6)]
		void DownloadVendorAvailability(string poNumber);
	}
}