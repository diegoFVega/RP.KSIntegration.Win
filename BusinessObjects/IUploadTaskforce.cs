using System;
using System.Runtime.InteropServices;

namespace BusinessObjects
{
	[Guid("329D669C-772B-405A-BC77-B847D3680FBF"),
		 InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IUploadTaskforce
	{
		[DispId(1)]
		string TestCreateObject(string testString = null);

		[DispId(2)]
		string NotifyTransferInvoices(string invoiceDate);

		[DispId(3)]
		string UpdateInvoiceInformation(string invoiceDate);

		[DispId(4)]
		void SendPurchaseOrderReference(int purchaseOrderItemId, string reference);
	}
}