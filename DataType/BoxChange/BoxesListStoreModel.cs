using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataType.BoxChange
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BoxesListStoreModel : List<BoxCodeToChange>
	{
	}
}