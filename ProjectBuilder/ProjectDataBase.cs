using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ProjectBuilder
{

	public abstract class ProjectDataBaseA<T> : ProjectDataBase<T>//, IProjDataC
	{
		internal string nl = Util.nl;

		[XmlIgnore]
		public abstract string ID { get; set; }

		[XmlIgnore]
		public abstract string Description { get; set; }

		public IDInfo GetID()
		{
			return new IDInfo(ID, Description);
		}
	}

	public abstract class ProjectDataBaseB<T1, T2> : ProjectDataBaseA<T1> where T2 : class, IProjDataB
	{
		[XmlIgnore]
		public abstract List<T2> ItemList { get; }

		public T2 FindChild(string number)
		{
			if (number == null) { return null; }

			return ItemList.Find(x => x.ID.Equals(number));
		}

		public List<FindItem> FindItems(UserProj uProj, int level)
		{
			List<FindItem> FoundList = new List<FindItem>();

			level++;

			if (IDInfo.NumberIsAll(uProj[level]))
			{
				foreach (T2 oneItem in ItemList)
				{
					List<FindItem> foundItems = oneItem.FindItems(uProj, level);

					FoundList.Add(new FindItem(
						new IDInfo(oneItem.ID, oneItem.Description), foundItems));
				}
			}
			else
			{
				if (uProj[level] != null)
				{
					T2 oneItem = FindChild(uProj[level].ID);

					if (oneItem != null)
					{
						List<FindItem> foundItems = oneItem.FindItems(uProj, level);
						FoundList.Add(new FindItem(
							new IDInfo(oneItem.ID, oneItem.Description), foundItems));
					}
				}
			}

			return FoundList;
		}

		public bool Add(ProjData pData)
		{
			if (ProjData == null || ProjectData.Exists(pData.Project)) return false
		}

		public void Sort()
		{
			if (ItemList == null
				|| ItemList.Count == 0) return;

			foreach (T2 oneItem in ItemList)
			{
				oneItem.Sort();
			}

			ItemList.Sort((x, y) => x.ID.CompareTo(y.ID));

		}


	}
}