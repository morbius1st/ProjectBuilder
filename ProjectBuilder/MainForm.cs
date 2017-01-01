using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectBuilder
{
	public partial class ProjectBuilderForm : Form
	{
		enum TestType
		{
			UserProjects,
			ProjectData
		}

		public ProjectBuilderForm()
		{
			InitializeComponent();
		}

		

		private void OK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		internal void SetMessageText(string text)
		{
			tbMessage.Text = text;
		}

		internal void SetFieldText(string text)
		{
			tbField.Text = text;


		}

		private void ProjectBuilderForm_Shown(object sender, EventArgs e)
		{
			TestType t;

			t = TestType.ProjectData;
			//			t = TestType.UserProjects;

			switch (t)
			{
				case TestType.ProjectData:
					TestProjectData tpd = new TestProjectData(this);
					tbMessage.Refresh();
					tbField.Refresh();
					break;
				case TestType.UserProjects:
					TestUserProjects tup = new TestUserProjects(this);
					tbMessage.Refresh();
					tbField.Refresh();
					break;
			}

		}
	}
}
