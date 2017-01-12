using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBuilder
{
	/* basic class structure
	 * 
	 * "master class"
	 * UserProjects - holds the collection of UserProjectsUser
	 * 
	 * UserProjectsUser
	 *      holds field: user name
	 *      holds collection of UserProjecstUserProject
	 * 
	 * UserProjecstUserProject
	 *      holds the individual fields
	 *      for a single project
	 * 
	 * uProject
	 * 
	 */

	/// <summary>
	/// Methods for working with a UserProject XML file
	/// </summary>
	/// <remarks>
	/// Method Summary:<br/>
	/// <b>UserExists:</b> Determines if a user exists<br/>
	/// <b>FindUserProjects:</b> Find all projects for a user<br/>
	/// <b>FindUserProjects(ListType):</b> Finds all projects for a user of ListType (All, Active, Inactive)<br/>
	/// </remarks>
	public partial class UserProjects
	{
		public bool UserExists(UserProj uProj)
		{
			return User.Exists(x => x.Name.Equals(uProj.UserName));
		}

		// find a user and all of their projects
		private UserProjectsUser FindUserProjects(UserProj uProj)
		{
			return User.Find(x => x.Name.Equals(uProj.UserName));
		}

		/// <summary>
		/// Find a user and their projects by type - all, active, or inactive
		/// </summary>
		/// <param name="uProj">User Project Data</param>
		/// <param name="listProjects">Project Listing Type</param>
		/// <returns></returns>
		public UserProjectsUser FindUserProjects(UserProj uProj, ListingTypeStruct listProjects)
		{

			if (listProjects == ListingType.All)
			{
				return FindUserProjects(uProj);
			}

			bool active = (listProjects == ListingType.Active);

			UserProjectsUser userProjectsAll = this.FindUserProjects(uProj);
			UserProjectsUser userProjectsActive = null;

			if (userProjectsAll != null)
			{
				userProjectsActive = new UserProjectsUser();

				foreach (UserProjectsUserProject p in userProjectsAll.Project)
				{
					if (p.Active == active)
					{
						userProjectsActive.Project.Add(p);
					}
				}
			}
			return userProjectsActive;
		}

		public bool AddUser(UserProj uProj)
		{
			if (this.UserExists(uProj))
				return false;

			UserProjectsUser user = new UserProjectsUser(uProj);

			User.Add(user);

			return true;
		}


		// delete a user if:
		// user exists (and name provided is not empty)
		// user has no projects
		public bool AddProjectToUser(UserProj uProj)
		{
			if (!this.UserExists(uProj))
			{
				// user does not already exist
				// add this user
				if (!this.AddUser(uProj))
				{
					return false;
				}
			}

			// get a reference to the user's data
			UserProjectsUser upu = this.FindUserProjects(uProj);

			// does the project already exist
			if (upu.FindProject(uProj) != null)
			{
				return false;
			}

			// got a UserProjectUser class to hold the new project
			// and the project does not already exist
			// add a project
			upu.AddProject(uProj);

			return true;
		}

		public bool DeleteUser(UserProj uProj)
		{
			if (String.IsNullOrWhiteSpace(uProj.UserName) ||
				!this.UserExists(uProj))
				return false;

			UserProjectsUser upu = this.userField.Find(x => x.Name.Equals(uProj.UserName));

			//			if (upu.Project.Count != 0)
			//				return false;

			return this.userField.Remove(upu);

		}

		public bool DeleteProjectFromUser(UserProj uProj)
		{
			// get a reference to the users list of projects
			UserProjectsUser upu = this.FindUserProjects(uProj);

			// found nothing, no such user
			if (upu == null || !upu.DeleteProject(uProj))
				return false;

			if (upu.Project.Count == 0)
				DeleteUser(uProj);

			return true;
		}

		public bool DeleteAllProjectsFromUser(UserProj uProj)
		{
			// find a user and their projects
			UserProjectsUser upu = FindUserProjects(uProj);

			// found nothing, no such user
			if (upu == null)
				return false;

			upu.DeleteAllProjects();

			return true;
		}

		// delete from all users any reference to the project number
		// provided regardless of the task, phase, or building
		public void DeleteProjectFromAllUsersByNumber(UserProj uProj)
		{
			if (!String.IsNullOrWhiteSpace(uProj.ProjKey.ID))
			{
				List<UserProj> usersToDelete = new List<UserProj>(0);

				foreach (UserProjectsUser upu in this.userField)
				{
					upu.Project.RemoveAll(x => x.Number.Equals(uProj.ProjKey));

					if (upu.Project.Count == 0)
					{
						usersToDelete.Add(new UserProj(upu.Name));
					}
				}

				// now delete any users that ended up with zero projects
				if (usersToDelete.Count > 0)
				{
					// we 1 or more users to delete
					foreach (UserProj u in usersToDelete)
					{
						DeleteUser(u);
					}
				}
			}
		}

		// delete from all users any reference to the project number
		// provided regardless of the task, phase, or building
		public void DeleteProjectFromAllUsersByNumberAndTask(UserProj uProj)
		{
			if (!String.IsNullOrWhiteSpace(uProj.ProjKey.ID))
			{
				List<UserProj> usersToDelete = new List<UserProj>(0);

				foreach (UserProjectsUser upu in this.userField)
				{
					upu.Project.RemoveAll(x => x.Number.Equals(uProj.ProjKey) && x.Task.Equals(uProj.TaskKey));

					if (upu.Project.Count == 0)
					{
						usersToDelete.Add(new UserProj(upu.Name));
					}
				}

				// now delete any users that ended up with zero projects
				if (usersToDelete.Count > 0)
				{
					// we 1 or more users to delete
					foreach (UserProj u in usersToDelete)
					{
						DeleteUser(u);
					}
				}
			}
		}

		public bool SetUserProjectToCurrent(UserProj uProj)
		{
			// set the user / project per uProj to current
			// find the projects for this user
			UserProjectsUser upu = this.FindUserProjects(uProj);

			if (upu == null)
			{
				// no such user
				return false;
			}

			return upu.SetProjectCurrent(uProj, true);
		}

		public bool SetUserProjectToNotCurrent(UserProj uProj)
		{
			// set the user / project per uProj to current
			// find the projects for this user
			UserProjectsUser upu = this.FindUserProjects(uProj);

			if (upu == null)
			{
				// no such user
				return false;
			}

			return upu.SetProjectCurrent(uProj, false);
		}

		public bool SetUserProjectToActive(UserProj uProj)
		{
			// set the user / project per uProj to Active
			UserProjectsUser upu = this.FindUserProjects(uProj);

			if (upu == null)
			{
				// no such user
				return false;
			}

			return upu.SetProjectActiveInactive(uProj, true);
		}

		public bool SetUserProjectToInactive(UserProj uProj)
		{
			// set the user / project per the uProj to inactive
			UserProjectsUser upu = this.FindUserProjects(uProj);

			if (upu == null)
			{
				// user not found
				return false;
			}

			return upu.SetProjectActiveInactive(uProj, false);
		}

		public int ProjectCountByUser(UserProj uProj)
		{
			return this.FindUserProjects(uProj).Project.Count;
		}

	}

	public partial class UserProjectsUser
	{
		// construct empty UserProjectsUser
		public UserProjectsUser()
		{
			Name = "";
			Project = new List<UserProjectsUserProject>();
		}

		// construct UserProjectsUser with just a name
		public UserProjectsUser(UserProj uProj)
		{
			Name = uProj.UserName;
			Project = new List<UserProjectsUserProject>();
		}


		// add a project (for a user)
		public void AddProject(UserProj uProj)
		{
			Project.Add(UserProjectsUserProject.CreateProject(uProj));
		}

		// delete a project (from a user)
		public bool DeleteProject(UserProj uProj)
		{
			UserProjectsUserProject upup = Project.Find(x => (x.Active == uProj.Active &&
				x.Number.Equals(uProj.ProjKey) &&
				x.Task.Equals(uProj.TaskKey) &&
				x.Phase.Equals(uProj.PhaseKey) &&
				x.Building.Equals(uProj.BldgKey)));

			return projectField.Remove(upup);
		}

		public void DeleteAllProjects()
		{
			if (this.projectField.Count > 0)
				this.projectField.RemoveRange(0, projectField.Count);
		}

		public UserProjectsUserProject FindProject(UserProj uProj)
		{
			if (uProj.Equals(default(UserProj)) || Name != uProj.UserName)
				return null;

			foreach (UserProjectsUserProject p in Project)
			{
				if (p.Match(uProj))
					return p;
			}
			return null;
		}

		public bool SetProjectActiveInactive(UserProj uProj, bool active)
		{
			if (projectField == null || projectField.Count == 0)
				return false;

			foreach (UserProjectsUserProject p in projectField)
			{
				if (p.Match(uProj))
				{
					p.Active = active;
					return true;
				}
			}

			return true;
		}

		// set a project to current or not current - if it exists
		// insures that only 1 project can be current and allows
		// that all projects can be not-current
		// it is OK to have all projects not-current
		public bool SetProjectCurrent(UserProj uProj, bool current)
		{
			if (projectField == null ||
				projectField.Count == 0)
				return false;

			bool result = false;

			foreach (UserProjectsUserProject p in projectField)
			{

				result = result || p.Match(uProj);

				p.Current = p.Match(uProj) && current;
			}

			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("user: " + this.nameField);
			sb.Append(Util.nl + "projects:");


			foreach (UserProjectsUserProject p in this.Project)
			{
				sb.Append(Util.nl);
				sb.Append(p.ToStringWithActive());
			}


			return sb.ToString();
		}
	}


	public partial class UserProjectsUserProject
	{
		public static UserProjectsUserProject CreateProject(UserProj uProj)
		{
			UserProjectsUserProject upup = new UserProjectsUserProject();

			upup.Current = uProj.Current;
			upup.CurrentSpecified = true;
			upup.Active = uProj.Active;
			upup.ActiveSpecified = true;
			upup.Number = uProj.ProjKey.ID;
			upup.Task = uProj.TaskKey.ID;
			upup.Phase = uProj.PhaseKey.ID;
			upup.Building = uProj.BldgKey.ID;

			return upup;
		}

		public bool Match(UserProj uProj)
		{
			return this.Number.Equals(uProj.ProjKey.ID) &&
				this.Task.Equals(uProj.TaskKey.ID) &&
				this.Phase.Equals(uProj.PhaseKey.ID) &&
				this.Building.Equals(uProj.BldgKey.ID);
		}

		public override string ToString()
		{
			string phase = phaseField ?? "";
			string building = buildingField ?? "";

			phase = Util.Diamonds.Substring(phase.Length) + phase;
			building = Util.Diamonds.Substring(building.Length) + building;

			return String.Format("{0}-{1}-{2}-{3}",
					numberField ?? "", taskField ?? "", phase, building);
		}

		public string ToStringWithActive()
		{
			//			string active = activeField.ToString();

			string project = this.ToString();

			return String.Format("active? {0} project: {1}", activeField, project);


		}
	}
}
