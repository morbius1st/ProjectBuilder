using System.Collections.Generic;

namespace ProjectBuilder
{
	public interface IProjDataA
	{
		
		bool Update(ProjData pdNew);
	}

	public interface IProjDataB : IProjDataA
	{
		string ItemID { get; }
		string ItemDescription { get; }
		List<FindItem> FindItems(UserProj uProj, int level);
		void Sort();
		IDInfo GetID();
	}
}