using System.Collections.Generic;

namespace ProjectBuilder
{
	public interface IPDInfo
	{
		string itemNumber { get; }
		string itemDescription { get; }
		List<FindItem> FindItems(uProject upx, int level);
		void Sort();
	}
}