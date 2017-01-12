using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectBuilder
{
	public class TestUserProjects
	{
		private ProjectBuilderForm MainForm;
		private UserProjects up;

		private string xmlUserFile = @"D:\Users\Jeff\Documents\Programming\_Office Standards\ProjectUserData\UserProjects.xml";

		internal TestUserProjects(ProjectBuilderForm form)
		{
			MainForm = form;

			MainForm.SetMessageText("This is message text from UserProject");
			MainForm.SetFieldText("This is field text from UserProject");
		}

		public StringBuilder UserProjectsTests()
		{
			string nl = Util.nl;

			UserProj uProj;

			StringBuilder sb = new StringBuilder();

			up = UserProjects.LoadFromFile(xmlUserFile);

			string name = "jeffs";
			// default 
			uProj = new UserProj(name, false, false,
				new IDInfo("2099-999"), new IDInfo("00"),
				new IDInfo("1"), new IDInfo("A"));


			//			int[] testNumber = new[] { 1, 22, 1, 21, 1 };	// test flip current

			//			int[] testNumber = new[] { 1, 13, 1, 12, 1 };	// test flip active

			int[] testNumber = new[] { 899900, 1, 21, 1, 22, 1 };   // test flip current for non-current

			foreach (int test in testNumber)
			{
				switch (test)
				{
					case 0:     // list all projects
						sb.Append(ListAllUsersAndProjects());
						break;
					case 1:     // list all projects for a user by type: all
						sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
						break;
					case 2:     // list all project for a user by type: Active
						sb.Append(ListProjectsForUserByType(uProj, ListingType.Active));
						break;
					case 3:     // list all project for a user by type: Inactive
						sb.Append(ListProjectsForUserByType(uProj, ListingType.Inactive));
						break;
					case 11:    // add a project
						sb.Append(nl + AddProject(uProj));
						break;
					case 12:    // make project active
						sb.Append(nl + ActiveProject(uProj));
						break;
					case 13:    // make project inactive
						sb.Append(nl + InactiveProject(uProj));
						break;
					case 14:    // delete a project
						sb.Append(nl + DeleteProject(uProj));
						break;
					case 21:    // make a project current
						sb.Append(nl + CurrentProject(uProj));
						break;
					case 22:    // make a project not current
						sb.Append(nl + NotCurrentProject(uProj));
						break;
					case 31:    // delete a project from all users
						sb.Append(nl + DeleteProjectFromAllUsers(uProj));
						break;
					case 32:    // delete a project / task from all users
						sb.Append(nl + DeleteProjectAndTaskFromAllUsers(uProj));
						break;
					//					case 41:	// add a user - this is done via addProject
					//						sb.Append(nl + AddUser(uProj));
					//						break;
					case 42:    // delete a user
						sb.Append(nl + DeleteUser(uProj));
						break;
					case 99:    // save the file
						savefile();
						break;
					case 999900:    // set uProj: 2099-999 / 00 / 1 / A
						uProj = new UserProj(name, false, false,
							new IDInfo("2099-999"), new IDInfo("00"),
							new IDInfo("1"), new IDInfo("A"));
						break;
					case 899900:    // set uProj: 2099-999 / 00 / 1 / A
						uProj = new UserProj(name, false, false,
							new IDInfo("2098-999"), new IDInfo("00"),
							new IDInfo("1"), new IDInfo("A"));
						break;
					default:
						sb.Append(nl).Append("No tests selected");
						break;
				}
			}


			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			sb.Append(nl + AddProject(uProj));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			sb.Append(nl + ActiveProject(uProj));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			sb.Append(nl + InactiveProject(uProj));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			sb.Append(nl + DeleteProject(uProj));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			sb.Append(Util.nl + NotCurrentProject(uProj));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			sb.Append(Util.nl + CurrentProject(uProj));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			uProj = new uProject("alyx", false, false, "2099-999", "00", "1", "A");
			//
			//			sb.Append(Util.nl + AddProject(uProj));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.All));
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.Active));
			//			sb.Append(ListProjectsForUserByType(uProj, ListingType.Inactive));


			//			uProj = new uProject("jeff", false, false, "2099-999", "00", "1", "A");
			//
			//			sb.Append(ListAllUsersAndProjects());
			//			sb.Append(DeleteProjectFromAllUsers(uProj));

			//			sb.Append(DeleteProjectAndTaskFromAllUsers(uProj));

			//			

			//			sb.Append(DeleteUser(uProj));

			//			up.DeleteAllProjectsFromUser(uProj);

			//			sb.Append(ListAllUsersAndProjects());

			//			savefile();

			return sb;
		}

		public void savefile()
		{
			up.SaveToFile(xmlUserFile);
		}

		public StringBuilder DescribeProcedure(UserProj uProj, string message)
		{
			StringBuilder sb = new StringBuilder(message);

			sb.Append(Util.nl + FormatProjectNumber(uProj));
			sb.Append(Util.nl + "for user: ");
			sb.Append(uProj.UserName);

			return sb;
		}

		public StringBuilder DeleteUser(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Delete user"));

			if (up.DeleteUser(uProj))
				sb.Append(Util.nl + "\uD83D\uDC4D user deleted" + Util.nl);
			else
				sb.Append(Util.nl + "\uD83D\uDD93 user NOT deleted" + Util.nl);

			return sb;
		}

		public StringBuilder DeleteProjectAndTaskFromAllUsers(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Delete project/task from all users"));

			up.DeleteProjectFromAllUsersByNumberAndTask(uProj);

			return sb;
		}

		public StringBuilder DeleteProjectFromAllUsers(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Delete project from all users"));

			up.DeleteProjectFromAllUsersByNumber(uProj);

			return sb;
		}

		public StringBuilder NotCurrentProject(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Setting project not-current"));

			if (up.SetUserProjectToNotCurrent(uProj))
				sb.Append(Util.nl + "\uD83D\uDC4D project not-current" + Util.nl);
			else
				sb.Append(Util.nl + "\uD83D\uDD93 project NOT not-current" + Util.nl);

			return sb;
		}

		public StringBuilder CurrentProject(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Setting project current"));

			if (up.SetUserProjectToCurrent(uProj))
				sb.Append(Util.nl + "\uD83D\uDC4D project current" + Util.nl);
			else
				sb.Append(Util.nl + "\uD83D\uDD93 project NOT current" + Util.nl);

			return sb;
		}


		public StringBuilder ActiveProject(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Setting project active"));

			if (up.SetUserProjectToActive(uProj))
				sb.Append(Util.nl + "\uD83D\uDC4D project active" + Util.nl);
			else
				sb.Append(Util.nl + "\uD83D\uDD93 project NOT active" + Util.nl);

			return sb;
		}

		public StringBuilder InactiveProject(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Setting project inactive"));

			if (up.SetUserProjectToInactive(uProj))
				sb.Append(Util.nl + "\uD83D\uDC4D project inactive" + Util.nl);
			else
				sb.Append(Util.nl + "\uD83D\uDD93 project NOT inactive" + Util.nl);

			return sb;
		}

		public StringBuilder DeleteProject(UserProj uProj)
		{
			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Deleting project"));

			if (up.DeleteProjectFromUser(uProj))
				sb.Append(Util.nl + "\uD83D\uDC4D project deleted" + Util.nl);
			else
				sb.Append(Util.nl + "\uD83D\uDD93 project NOT deleted" + Util.nl);

			return sb;
		}

		public StringBuilder AddProject(UserProj uProj)
		{

			StringBuilder sb = new StringBuilder(Util.nl);

			sb.Append(DescribeProcedure(uProj, "Adding project"));

			if (up.AddProjectToUser(uProj))
			{
				sb.Append(Util.nl + "\uD83D\uDC4D project added" + Util.nl);
			}
			else
			{
				sb.Append(Util.nl + "\uD83D\uDD93project NOT added" + Util.nl);
			}

			return sb;
		}


		public static StringBuilder FormatProjectNumber(UserProj uProj)
		{

			string phase = uProj.PhaseKey.ID ?? "";
			string building = uProj.BldgKey.ID ?? "";

			phase = Util.Diamonds.Substring(phase.Length) + phase;
			building = Util.Diamonds.Substring(building.Length) + building;

			return new StringBuilder(String.Format("{0}-{1}-{2}-{3}",
					uProj.ProjKey.ID ?? "", uProj.TaskKey.ID ?? "", phase, building));
		}



		private StringBuilder FormatProjectNumber(UserProjectsUserProject p)
		{
			if (p == null)
				return new StringBuilder("");

			return FormatProjectNumber(new UserProj("", false, true,
				new IDInfo(p.Number), new IDInfo(p.Task),
				new IDInfo(p.Phase), new IDInfo(p.Building)));
		}



		private StringBuilder FormatProject(UserProjectsUserProject p)
		{
			return new StringBuilder(String.Format("\tproject: {0}   is Active? {1}  is current? {2}{3}",
				FormatProjectNumber(p), p.Active, p.Current, Util.nl));
		}


		private StringBuilder ListProjects(UserProjectsUser upu)
		{
			StringBuilder sb = new StringBuilder("");


			if (upu != null && upu.Project.Count > 0)
			{
				foreach (UserProjectsUserProject p in upu.Project)
				{
					sb.Append(FormatProject(p));
				}
			}
			else
			{
				sb.Append("no projects" + Util.nl);
			}

			return sb;
		}

		private StringBuilder ListAllUsersAndProjects()
		{
			StringBuilder sb = new StringBuilder();

			List<UserProjectsUser> upUsers = up.User;

			List<UserProjectsUserProject> projects;

			sb.Append("count: " + upUsers.Count + Util.nl);

			for (int i = 0; i < upUsers.Count; i++)
			{
				sb.Append("user " + i + ":  is: " + upUsers[i].Name);

				projects = upUsers[i].Project;

				sb.Append(" and has " + projects.Count + " projects" + Util.nl);

				for (int j = 0; j < projects.Count; j++)
				{
					UserProjectsUserProject p = projects[j];
					sb.Append(FormatProject(p));
				}
			}

			return sb;
		}

		private StringBuilder ListProjectsForUserByType(UserProj uProj, ListingTypeStruct listingType)
		{
			StringBuilder sb = new StringBuilder();

			string listingTypeName = listingType.Name;

			UserProjectsUser upu = up.FindUserProjects(uProj, listingType);

			if (upu != null && upu.Project != null
				&& upu.Project.Count > 0)
			{
				sb.Append(Util.nl + listingTypeName + " projects for " + uProj.UserName + ":");
				sb.Append(Util.nl + "project count: " + upu.Project.Count);
				sb.Append(Util.nl);
				sb.Append(ListProjects(upu));
			}
			else
			{
				sb.Append("user not found");
			}
			return sb;
		}

	}
}