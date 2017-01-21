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

	public abstract class ProjectDataBaseB<T1, T2> : ProjectDataBaseA<T1> where T2 : class, IProjDataB, new()
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

		public bool AddItem(ProjData pData, int level = 0)
		{
			if (pData == null) return false;

			level++;

			T2 item = FindItem(pData.Project, level);

			if (item == null)
			{
				item = new T2();

				item.ID = pData.Project[level]?.ID ?? "";
				item.Description = pData.Project[level]?.Description ?? "";

				ItemList.Add(item);
			}

			item.AddItem(pData, level);

			return true;
		}

		public T2 FindItem(UserProj userProj, int level)
		{

			return ItemList.Find(x => x.ID.Equals(userProj[level]?.ID));
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