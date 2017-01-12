using System.Collections;
using System.Text;
using System.Windows.Forms.VisualStyles;

using static ProjectBuilder.Util;

namespace ProjectBuilder
{

	public class UserProj : IEnumerable
	{
		internal const int PROJ = 0;
		internal const int TASK = 1;
		internal const int PHAZ = 2;
		internal const int BLDG = 3;
		internal const int IDMAX = BLDG;

		private string	_username;		// user name
		private bool	_current;		// the user's current project
		private bool	_active;		// project is active
		private IDInfo[] _idinfo = new IDInfo[4];    // id components

		public UserProj()
			: this(null, false, false, null, null, null, null) { }

		public UserProj(string username, bool current, bool active,
			IDInfo projid, IDInfo taskid, IDInfo phaseid, IDInfo bldgid)
		{
			_username = username;
			_current = current;
			_active = active;

			_idinfo[PROJ] = projid;
			_idinfo[TASK] = taskid;
			_idinfo[PHAZ] = phaseid;
			_idinfo[BLDG] = bldgid;
		}

		public UserProj(IDInfo taskid,
			IDInfo phaseid, IDInfo bldgid)
			: this(null, taskid, phaseid, bldgid) { }


		public UserProj(IDInfo projid, IDInfo taskid,
			IDInfo phaseid, IDInfo bldgid)
			: this(null, false, false, projid, taskid, phaseid, bldgid) { }

		public UserProj(string projid, string taskid, string phaseid, string bldgid)
			: this(null, false, false,
				new IDInfo(projid),
				new IDInfo(taskid),
				new IDInfo(phaseid),
				new IDInfo(bldgid))
		{ }

		public UserProj(string username)
			: this(username, false, false, null, null, null, null) { }

		public IDInfo this[int index]
		{
			get
			{
				if (index < 0 || index > 3) { return null; }
				return _idinfo[index];
			}
			set
			{
				if (index < 0 || index > 3) { return; }
				_idinfo[index] = value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _idinfo.GetEnumerator();
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

		public IDInfo ProjKey
		{
			get { return _idinfo[PROJ]; }
			set { _idinfo[PROJ] = value; }
		}

		public IDInfo TaskKey
		{
			get { return _idinfo[TASK]; }
			set { _idinfo[TASK] = value; }
		}

		public IDInfo PhaseKey
		{
			get { return _idinfo[PHAZ]; }
			set { _idinfo[PHAZ] = value; }
		}

		public IDInfo BldgKey
		{
			get { return _idinfo[BLDG]; }
			set { _idinfo[BLDG] = value; }
		}

		public UserProj Clone()
		{
			UserProj upn = new UserProj
			{
				UserName = UserName,
				Active = Active,
				Current = Current
			};


			for (int i = 0; i <= BLDG; i++)
			{
				if (_idinfo[i] != null)
					upn._idinfo[i] = new IDInfo(_idinfo[i].ID, _idinfo[i].Description);
			}
			return upn;
		}

		internal bool HasNumber()
		{
			return !(IDInfo.NumberIsNullOrEmpty(_idinfo[TASK]) &&
				IDInfo.NumberIsNullOrEmpty(_idinfo[PHAZ]) &&
				IDInfo.NumberIsNullOrEmpty(_idinfo[BLDG]));
		}

		internal bool HasDescription()
		{
			return !(IDInfo.DescriptionIsNullOrEmpty(_idinfo[TASK]) &&
				IDInfo.DescriptionIsNullOrEmpty(_idinfo[PHAZ]) &&
				IDInfo.DescriptionIsNullOrEmpty(_idinfo[BLDG]));
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			int column = 0;

			sb.Append(FormatItemDividerN());
			sb.Append(FormatItemN(column, "username", _username));
			sb.Append(FormatItemN(column, "current", _current.ToString()));
			sb.Append(FormatItemN(column, "active", _active.ToString()));
			sb.Append(FormatItemN(column, "projectnumber", _idinfo[PROJ].ID));
			sb.Append(FormatItemN(column, "task", _idinfo[TASK].ID));
			sb.Append(FormatItemN(column, "phase", _idinfo[PHAZ].ID));
			sb.Append(FormatItemN(column, "building", _idinfo[BLDG].ID));

			return sb.ToString();
		}

	}
}