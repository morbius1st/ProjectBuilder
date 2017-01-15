using System;
using System.Text;

namespace ProjectBuilder
{
	internal static class Util
	{
		public static string nl = System.Environment.NewLine;
		public const string Diamonds = "♦♦♦♦♦♦";
		public const int ColumnAdjust = 2;
		public const string AODWG = ".dwg";
		public static int Column = 0;
		private const int DASHWIDTH = 120;
		private const int COLON_COLUMN = 48;
		public const string All = "*";
		public const string Keep = All;


		public static bool IsKeepAsIs(string value)
		{
			return value != null && value == Keep;
		}

		public static StringBuilder FormatItemDivider()
		{
			return new StringBuilder(new String('-', DASHWIDTH));
		}

		public static StringBuilder FormatItemDividerN()
		{
			return new StringBuilder(nl).Append(FormatItemDivider());

		}

		public static StringBuilder FormatItem(string description, string value)
		{
			return FormatItem(0, description, value);
		}

		public static StringBuilder FormatItem(int col, string description, string value)
		{
			string width = (col - COLON_COLUMN).ToString();

			if (String.IsNullOrWhiteSpace(value))
			{
				return new StringBuilder().AppendFormat("{0," + width + "}", description);
			}
			else if (value.Equals(":"))
			{
				value = "";
			}

			return new StringBuilder().AppendFormat("{0," + width + "}: {1}", description, value);
		}

		public static StringBuilder FormatItemN(string description)
		{
			return new StringBuilder(nl).Append(FormatItem(0, description, ""));
		}

		public static StringBuilder FormatItemN(string description, string value)
		{
			return new StringBuilder(nl).Append(FormatItem(0, description, value));
		}

		public static StringBuilder FormatItemN(int col, string description, string value)
		{
			StringBuilder sb = new StringBuilder(nl);

			sb.Append(ColumnOffset(col));

			return sb.Append(FormatItem(col, description, value));
		}

		public static StringBuilder FormatItemN(int col, string description)
		{
			StringBuilder sb = new StringBuilder(nl);

			sb.Append(ColumnOffset(col));

			return sb.Append(description);
		}

		public static StringBuilder ColumnOffset(int col)
		{
			StringBuilder sb = new StringBuilder();

			if (col > 0 && col < 24)
			{
				for (int i = 0; i < col; i++)
				{
					sb.Append(" ");
				}
			}
			return sb;
		}

		public static string FormatTaskPhaseBuilding(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatName(uProj.TaskKey.ID));

			sb.Append(FormatName(uProj.PhaseKey.ID));

			sb.Append(FormatName(uProj.BldgKey.ID));

			return sb.ToString();
		}

		public static string FormatName(string name)
		{
			return Diamonds.Substring(0, 6 - name.Length) + name;
		}
	}
}