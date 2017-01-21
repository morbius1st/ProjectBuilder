using static System.String;
using static ProjectBuilder.Util;

namespace ProjectBuilder
{
	public class IDInfo
	{
		private string _id;
		public string Description;

		public IDInfo(string id, string description) : this(id)
		{
			Description = description ?? "";
		}

		public IDInfo(string id)
		{
			_id = id?.Trim() ?? "";
		}

		public string ID
		{
			get { return _id; }
			set { _id = value?.Trim() ?? ""; }
		}

		public IDInfo Clone()
		{
			return new IDInfo(_id, Description);
		}

		internal static bool NumberIsAll(IDInfo id)
		{
			return id?.ID != null && id.ID.Equals(All);
		}

		internal static bool NumberIsNullOrEmpty(IDInfo id)
		{
			return IsNullOrWhiteSpace(id?.ID);
		}

		internal static bool DescriptionIsNullOrEmpty(IDInfo id)
		{
			return IsNullOrWhiteSpace(id?.Description);
		}
	}
}