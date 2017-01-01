using System.Windows.Forms;

namespace ProjectBuilder
{
	public class TestUserProjects
	{
		private ProjectBuilderForm MainForm;

		internal TestUserProjects(ProjectBuilderForm form)
		{
			MainForm = form;

			MainForm.SetMessageText("This is message text from UserProject");
			MainForm.SetFieldText("This is field text from UserProject");
		}
	}
}