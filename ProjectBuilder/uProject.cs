using System.Collections;
using System.Text;
using System.Windows.Forms.VisualStyles;
using static System.String;
using static ProjectBuilder.Util;

namespace ProjectBuilder
{
	public class ProjNumInfo
	{
		private string _number;
		public string Description;

		public ProjNumInfo(string number, string description) : this(number)
		{
			Description = description;
		}

		public ProjNumInfo(string number)
		{
			_number = number.Trim();
		}

		public string Number
		{
			get { return _number; }
			set { _number = value.Trim(); }
		}

		public ProjNumInfo Clone()
		{
			return new ProjNumInfo(_number, Description);
		}

		internal static bool NumberIsAll(ProjNumInfo p)
		{
			return p?.Number != null && p.Number.Equals(all);
		}

		internal static bool NumberIsNullOrEmpty(ProjNumInfo p)
		{
			return IsNullOrWhiteSpace(p?.Number);
		}

		internal static bool DescriptionIsNullOrEmpty(ProjNumInfo p)
		{
			return IsNullOrWhiteSpace(p?.Description);
		}

	}


	public class uProject : IEnumerable
	{
		private const int PRJ = 0;
		private const int TSK = 1;
		private const int PHA = 2;
		private const int BLD = 3;
		private const int IDMAX = BLD;



		private string _username;           // user name
		private bool _current;          // the user's current project
		private bool _active;           // project is active
		private ProjNumInfo[] _projnuminfo = new ProjNumInfo[4];    // project number components

		public uProject()
			: this(null, false, false, null, null, null, null) { }

		public uProject(string username, bool current, bool active,
			ProjNumInfo projnumber, ProjNumInfo task, ProjNumInfo phase, ProjNumInfo building)
		{
			_username = username;
			_current = current;
			_active = active;

			_projnuminfo[PRJ] = projnumber;
			_projnuminfo[TSK] = task;
			_projnuminfo[PHA] = phase;
			_projnuminfo[BLD] = building;
		}

		public uProject(ProjNumInfo task,
			ProjNumInfo phase, ProjNumInfo building)
			: this(null, task, phase, building) { }


		public uProject(ProjNumInfo projnumber, ProjNumInfo task,
			ProjNumInfo phase, ProjNumInfo building)
			: this(null, false, false, projnumber, task, phase, building) { }

		public uProject(string projnumber, string task, string phase, string building)
			: this(null, false, false,
				new ProjNumInfo(projnumber),
				new ProjNumInfo(task),
				new ProjNumInfo(phase),
				new ProjNumInfo(building))
		{ }

		public uProject(string username)
			: this(username, false, false, null, null, null, null) { }

		public ProjNumInfo this[int index]
		{
			get
			{
				if (index < 0 || index > 3) { return null; }
				return _projnuminfo[index];
			}
			set
			{
				if (index < 0 || index > 3) { return; }
				_projnuminfo[index] = value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _projnuminfo.GetEnumerator();
		}

		public string UserName
		{
			get { return _username; }
			set { _username = value; }
		}

		public bool Current
		{
			get { return _current; }
			set { _current = value; }
		}

		public bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public ProjNumInfo ProjNumber
		{
			get { return _projnuminfo[PRJ]; }
			set { _projnuminfo[PRJ] = value; }
		}

		public ProjNumInfo Task
		{
			get { return _projnuminfo[TSK]; }
			set { _projnuminfo[TSK] = value; }
		}

		public ProjNumInfo Phase
		{
			get { return _projnuminfo[PHA]; }
			set { _projnuminfo[PHA] = value; }
		}

		public ProjNumInfo Building
		{
			get { return _projnuminfo[BLD]; }
			set { _projnuminfo[BLD] = value; }
		}

		public uProject Clone()
		{
			uProject upn = new uProject
			{
				UserName = UserName,
				Active = Active,
				Current = Current
			};


			for (int i = 0; i <= BLD; i++)
			{
				if (_projnuminfo[i] != null)
					upn._projnuminfo[i] = new ProjNumInfo(_projnuminfo[i].Number, _projnuminfo[i].Description);
			}
			return upn;
		}

		internal bool HasNumber()
		{
			return !(ProjNumInfo.NumberIsNullOrEmpty(_projnuminfo[TSK]) &&
				ProjNumInfo.NumberIsNullOrEmpty(_projnuminfo[PHA]) &&
				ProjNumInfo.NumberIsNullOrEmpty(_projnuminfo[BLD]));
		}

		internal bool HasDescription()
		{
			return !(ProjNumInfo.DescriptionIsNullOrEmpty(_projnuminfo[TSK]) &&
				ProjNumInfo.DescriptionIsNullOrEmpty(_projnuminfo[PHA]) &&
				ProjNumInfo.DescriptionIsNullOrEmpty(_projnuminfo[BLD]));
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			int column = 0;

			sb.Append(FormatItemDividerN());
			sb.Append(FormatItemN(column, "username", _username));
			sb.Append(FormatItemN(column, "current", _current.ToString()));
			sb.Append(FormatItemN(column, "active", _active.ToString()));
			sb.Append(FormatItemN(column, "projectnumber", _projnuminfo[PRJ].Number));
			sb.Append(FormatItemN(column, "task", _projnuminfo[TSK].Number));
			sb.Append(FormatItemN(column, "phase", _projnuminfo[PHA].Number));
			sb.Append(FormatItemN(column, "building", _projnuminfo[BLD].Number));

			return sb.ToString();
		}

	}
}