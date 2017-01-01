using System.Windows.Forms;

namespace ProjectBuilder
{
	public class TestProjectData
	{
		private ProjectBuilderForm MainForm;

		internal TestProjectData(ProjectBuilderForm form)
		{
			MainForm = form;

			MainForm.SetMessageText("This is message text from ProjectData");
			MainForm.SetFieldText("This is field text from ProjectData");
		}
	}
}