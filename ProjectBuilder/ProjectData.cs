using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

using static System.String;

using static ProjectBuilder.Util;
using static ProjectBuilder.UserProj;
using static ProjectBuilder.IDInfo;
using System.IO;

namespace ProjectBuilder
{
	/*
	 * Organization:
	 * root class:
	 * ProjectData
	 *    +-> Project -> ProjectDataProject
	 *    +-> Tasks = list<ProjectDataTask>
	 *    |
	 *    v
	 * ProjectDataTask
	 *    +-> ID (Task ID)
	 *    +-> Description
	 *    +-> Phase = list<ProjectDataTaskPhase>
	 *    | 
	 *    v
	 * ProjectDataTaskPhase
	 *    +-> ID (Phase ID)
	 *    +-> Description
	 *    +-> Building = list<ProjectDataTaskPhaseBldg>
	 *    |
	 *    v
	 * ProjectDataTaskPhaseBldg
	 *    +-> ID (Bldg ID)
	 *    +-> Description
	 *    +-> CDFolder
	 *    +-> SheetNumberFormat
	 *    +-> Location
	 *    |
	 *    v
	 * ProjectDataTaskPhaseBldgLocation
	 *    +-> AutoCAD -> ProjectDataTaskPhaseBldgLocationAutoCAD
	 *	  |			+-> Revit -> ProjectDataTaskPhaseBldgLocationRevit
	 *    |		    |
	 *    |			v
	 *    |		ProjectDataCDPackageTaskPhaseBuildingRevit
	 *    |		   +-> CDModelFile -> PathInfo -> Path
	 *    |		   +-> LibraryModelFile -> PathInfo -> Path
	 *    |		   +-> KeynoteFile -> PathInfo -> Path
	 *    |		   +-> LinkedFolder -> PathInfo -> Path
	 *    |		   +-> XrefFolder -> PathInfo -> Path
	 *    v  
	 * ProjectDataCDPackageTaskPhaseBuildingLocationAutoCAD
	 *    +-> SheetFolder -> PathInfo -> Path
	 *    +-> XrefFolder -> PathInfo -> Path
	 *    +-> DetailFolder -> PathInfo -> Path
	 *    +-> BorderFile -> PathInfo -> Path
	 * 
	 * ProjectDataProject
	 *    +-> ID (Project ID i.e. Project Number)
	 *    +-> Description
	 *    +-> RootFolder	
	 * 
	 *  
	 * needed methods
	 * @ ProjectData
	 * ✔ create project data file => bool (T or F)
	 * 
	 * ✔ Exists(uProject)
	 * 
	 * ✔ Find(uProject) => List<uProject>
	 * 
	 * ✔ GetProjectInfo(uProject) => ProjectDataProjectInformation
	 * ✔ GetShtNumFmt(uProject [specific] ) => string or null 
	 * 
	 * ✔ GetAcadLocationInfo(uProject [specific] ) => ProjectDataCDPackagesTaskPhaseBuildingLocationAutoCAD
	 * ✔ GetRevitLicationInfo(uProject [specific] ) => ProjectDataCDPackagesTaskPhaseBuildingLocationRevit
	 * 
	 * ✔ Add(uProjectData) => bool
	 * 
	 * ✔ DeleteProject(uProject) => bool
	 * 
	 * ✔ MakeChangeList(list<uProject> existing info, List<uProject> new info) => ChangeList
	 * 
	 *		change the task / phase / building - number and/ or descriptions
	 * ✔ ChangeProject(ChangeList) => out int[]
	 * 
	 * ✔ ChangeAcadLocation(uProjectData) => bool (adds if does not already exist)
	 * 
	 * ✔ ChangeRevitLocation(uProjectData) => bool (adds if does not already exist)
	 * 
	 * ✔ Sort the data file => void
	 * 
	 * 
	 *         
	 * setCDFolder(task, phase, building) => bool
	 * setShtNumFmt(task, phase, building) = bool
	 * setAcadLocation(uTask, uPhase, uBuilding, 
	 *         ProjectDataCDPackagesTaskPhaseBuildingLocationAutoCAD) => bool
	 * setRevitLocation(uTask, uPhase, uBuilding, 
	 *         ProjectDataCDPackagesTaskPhaseBuildingLocationRevit) => bool
	 * 
	 * deleteTask(task) => bool
	 * deletePhase(task, phase) => bool
	 * deleteBuilding(task, phase, building) => bool
	 * 
	 * hasChildren(task, phase) => bool
	 * 
	 * 
	 * project data
	 *   ItemID
	 *   ItemDescription
	 *   ItemList
	 * 
	 * 
	 * 
	 * 
	 */

//------------------------------------------------------------------------
	// holds the project data information
	public partial class ProjectData : ProjectDataBaseB<ProjectData, ProjectDataTask>, IProjDataB
	{
		private string xmlFile;
		private bool configured = false;

		[XmlIgnore]
		public override string ID
		{
			get { return Project.ID; }

			set { Project.ID = value; }
		}

		[XmlIgnore]
		public override string Description
		{
			get { return Project.Description; }
			set { Project.Description = value; }
		}

		[XmlIgnore]
		public override List<ProjectDataTask> ItemList => this.Tasks;

		public string XmlFile => xmlFile;

		public bool Configured => configured;

		// parameterless constructor
		internal ProjectData()
		{
			Tasks = new List<ProjectDataTask>();
		}

		// create new based on Basic project information
		internal ProjectData(ProjData pData)
		{
			Project = new ProjectDataProject(pData);
		}

		private void SetXMLFile(string file)
		{
			xmlFile = file;
			configured = true;
		}

		public static ProjectData CreateFile(string fileName, ProjData pData)
		{
			ProjectData pd = new ProjectData(pData);

			pd.SetXMLFile(fileName);

			pd.Save();

			return pd;
		}

		internal static ProjectData LoadFile(string fileName)
		{
			if (!File.Exists(fileName))
				return null;

			ProjectData pd = LoadFromFile(fileName);

			pd.SetXMLFile(fileName);

			return pd;
		}


		// if data is null or project exists => false
		// if task exists
		//  |  +-> yes -> task.add
		//  +----> no -> add task -> task.add
//		internal bool Add(ProjData pData)
//		{
//			if (pData == null || Exists(pData.Project)) return false;
//
//			ProjectDataTask foundTask = FindTask(pData.Project);
//			ProjectDataTaskPhase foundPhase;
//
//			if (foundTask == null)
//			{
//				Tasks.Add(new ProjectDataTask(pData.Project));
//			}
//
//
//
//			return true;
//		}

		public void Save()
		{
			SaveToFile(XmlFile);
		}

		public bool DeleteProject(UserProj uProj)
		{
			bool result = false;

			ProjectDataTask foundTask = FindTask(uProj);

			if (foundTask != null)
			{
				result = foundTask.Delete(uProj);

				if (foundTask.Phase.Count == 0)
				{
					Tasks.Remove(foundTask);
				}

			}



		}

		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}

		public bool Exists(UserProj uProj)
		{
			if (uProj == null) return false;

			return GetBuilding(uProj) != null;
		}

		internal List<UserProj> Find(UserProj uProj, int level = 0)
		{
			List<FindItem> foundList = FindItems(uProj, level);
			List<UserProj> userProjects = new List<UserProj>();

			foreach (FindItem oneTask in foundList)
			{
				foreach (FindItem onePhase in oneTask.FoundItems)
				{
					foreach (FindItem oneBldg in onePhase.FoundItems)
					{
						userProjects.Add(new UserProj(null, oneTask.Item,
							onePhase.Item, oneBldg.Item));
					}
				}
			}
			return userProjects;
		}

		internal ProjectDataTask FindTask(UserProj uProj)
		{
			return Tasks.Find(x => x.ID.Equals(uProj.TaskKey.ID));
		}

		internal ProjectDataTaskPhaseBldg GetBuilding(UserProj uProj)
		{
			ProjectDataTask foundTask = FindChild(uProj.TaskKey.ID);
			ProjectDataTaskPhaseBldg foundBldg = null;

			if (foundTask != null)
			{
				ProjectDataTaskPhase foundPhase = foundTask.FindChild(uProj.PhaseKey.ID);

				if (foundPhase != null)
				{
					foundBldg = foundPhase.FindChild(uProj.BldgKey.ID);
				}
			}

			return foundBldg;
		}

		internal ProjectDataProject GetProject()
		{
			return Project;
		}

		public override string ToString()
		{
			return Project.ToString();
		}
	}



//------------------------------------------------------------------------
	// holds the task information
	public partial class ProjectDataTask : ProjectDataBaseB<ProjectDataTask, ProjectDataTaskPhase>, IProjDataB
	{
		[XmlIgnore]
		public override List<ProjectDataTaskPhase> ItemList => Phase;

		// parameterless constructor required
		public ProjectDataTask()
		{
			Phase = new List<ProjectDataTaskPhase>();
		}

		private ProjectDataTask(string taskID, string taskDescription )
		{
			ID = taskID;
			Description = taskDescription;
		}

//		public ProjectDataTask(UserProj uProj) : this(uProj.TaskKey.ID, uProj.TaskKey.Description)
//		{
//			Phase = ProjectDataTaskPhase;
//		}


		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}

		internal ProjectDataTaskPhase FindPhase(UserProj uProj)
		{
			return Phase.Find(x => x.ID.Equals(uProj.PhaseKey.ID));
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemN(Column, "Task",":")).Append(FormatProjNum(ID, Description));

			return sb.ToString();
		}
	}



//------------------------------------------------------------------------
	// holds the phase information
	public partial class ProjectDataTaskPhase : ProjectDataBaseB<ProjectDataTaskPhase, ProjectDataTaskPhaseBldg>, IProjDataB
	{
		[XmlIgnore]
		public override List<ProjectDataTaskPhaseBldg> ItemList => Bldg;

		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}

		public ProjectDataTaskPhase()
		{
			Bldg = new List<ProjectDataTaskPhaseBldg>();
		}

		internal ProjectDataTaskPhaseBldg FindBldg(UserProj uProj)
		{
			return Bldg.Find(x => x.ID.Equals(uProj.BldgKey.ID));
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemN(Column, "Phase", ":")).Append(FormatProjNum(ID, Description));

			return sb.ToString();
		}
	}


//------------------------------------------------------------------------
	// holds the building information
	public partial class ProjectDataTaskPhaseBldg : ProjectDataBaseA<ProjectDataTaskPhaseBldg>, IProjDataB
	{

		// parameterless constructor is required
		public ProjectDataTaskPhaseBldg()
		{
			Location = new ProjectDataTaskPhaseBldgLocation();
		}

		internal ProjectDataTaskPhaseBldg(string id, string description)
		{
			ID = id ?? "";
			Description = description ?? "";
		}

		internal ProjectDataTaskPhaseBldg(UserProj uProj) : this(uProj.BldgKey.ID, uProj.BldgKey.Description)
		{
			CDFolder = "";
			SheetNumberFormat = "";
			Location = new ProjectDataTaskPhaseBldgLocation();
		}

		// building already added - just need to add the location data
		public bool AddItem(ProjData pData, int level)
		{
			return true;
		}

		public List<FindItem> FindItems(UserProj uProj, int level)
		{
			return null;
		}

		public void Sort()
		{
			return;
		}



		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}




		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemN(Column, "Building", ":")).Append(FormatProjNum(ID, Description));

			return sb.ToString();
		}
	}


//------------------------------------------------------------------------
	// holds the location information
	public partial class ProjectDataTaskPhaseBldgLocation : ProjectDataBase<ProjectDataTaskPhaseBldgLocation>, IProjDataA
	{

		public ProjectDataTaskPhaseBldgLocation()
		{
			AutoCAD = new ProjectDataTaskPhaseBldgLocationAutoCAD();
			Revit = new ProjectDataTaskPhaseBldgLocationRevit();
		}

		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}
	}


//------------------------------------------------------------------------
	// holds the autocad location information
	public partial class ProjectDataTaskPhaseBldgLocationAutoCAD : ProjectDataBase<ProjectDataTaskPhaseBldgLocationAutoCAD>, IProjDataA
	{
		// constructors
		internal ProjectDataTaskPhaseBldgLocationAutoCAD()
		{
			SheetFolder     = null;
			XrefFolder      = null;
			DetailFolder    = null;
			BorderFile      = null;
		}

		internal ProjectDataTaskPhaseBldgLocationAutoCAD(ProjData pData)
		{
			SheetFolder  = pData?.AutoCAD?.SheetFolder ?? null;
			XrefFolder   = pData?.AutoCAD?.XrefFolder ?? null;
			DetailFolder = pData?.AutoCAD?.DetailFolder ?? null;
			BorderFile   = pData?.AutoCAD?.BorderFile ?? null;
		}

		internal ProjDataAutoCAD Clone()
		{
			return new ProjDataAutoCAD(SheetFolder,
				XrefFolder,
				DetailFolder,
				BorderFile);
		}

		public bool Update(ProjData pData)
		{
			if (pData?.AutoCAD == null) return false;

			bool result = false;

			if (!IsKeepAsIs(pData.AutoCAD.SheetFolder))
			{
				SheetFolder = pData.AutoCAD.SheetFolder;
				result = true;
			}

			if (!IsKeepAsIs(pData.AutoCAD.XrefFolder))
			{
				XrefFolder = pData.AutoCAD.XrefFolder;
				result = true;
			}

			if (!IsKeepAsIs(pData.AutoCAD.DetailFolder))
			{
				DetailFolder = pData.AutoCAD.DetailFolder;
				result = true;
			}

			if (!IsKeepAsIs(pData.AutoCAD.BorderFile))
			{
				BorderFile = pData.AutoCAD.BorderFile;
				result = true;
			}

			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemDividerN());
			sb.Append(FormatItemN(Column, "AutoCAD Location Information"));

			Column += ColumnAdjust;

			sb.Append(FormatItemN(Column, "SheetFolder", SheetFolder));
			sb.Append(FormatItemN(Column, "XrefFolder", XrefFolder));
			sb.Append(FormatItemN(Column, "DetailFolder", DetailFolder));
			sb.Append(FormatItemN(Column, "BorderFile", BorderFile));

			Column -= ColumnAdjust;

			return sb.ToString();
		}
	}


//------------------------------------------------------------------------
	// holds revit location information
	public partial class ProjectDataTaskPhaseBldgLocationRevit : ProjectDataBase<ProjectDataTaskPhaseBldgLocationRevit>, IProjDataA
	{
		// constructors
		internal ProjectDataTaskPhaseBldgLocationRevit()
		{
			CDModelFile      = null;
			LibraryModelFile = null;
			KeynoteFile      = null;
			LinkedFolder     = null;
			XrefFolder       = null;
		}

		internal ProjectDataTaskPhaseBldgLocationRevit(ProjData pData)
		{
			CDModelFile      = pData?.Revit?.CDModelFile;
			LibraryModelFile = pData?.Revit?.LibraryModelFile;
			KeynoteFile      = pData?.Revit?.KeynoteFile;
			LinkedFolder     = pData?.Revit?.LinkedFolder;
			XrefFolder       = pData?.Revit?.XrefFolder;
		}

		internal ProjDataRevit Clone()
		{
			return new ProjDataRevit(CDModelFile,
				LibraryModelFile,
				KeynoteFile,
				LinkedFolder,
				XrefFolder);
		}
		  
		public bool Update(ProjData pData)
		{
			if (pData?.Revit == null) return false;

			bool result = false;

			if (!IsKeepAsIs(pData.Revit.CDModelFile))
			{
				CDModelFile = pData.Revit.CDModelFile;
				result = true;
			}

			if (!IsKeepAsIs(pData.Revit.LibraryModelFile))
			{
				LibraryModelFile = pData.Revit.LibraryModelFile;
				result = true;
			}

			if (!IsKeepAsIs(pData.Revit.KeynoteFile))
			{
				KeynoteFile = pData.Revit.KeynoteFile;
				result = true;
			}

			if (!IsKeepAsIs(pData.Revit.LinkedFolder))
			{
				LinkedFolder = pData.Revit.LinkedFolder;
				result = true;
			}

			if (!IsKeepAsIs(pData.Revit.XrefFolder))
			{
				XrefFolder = pData.Revit.XrefFolder;
				result = true;
			}

			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemN(Column, "Revit Location Information"));

			Column += ColumnAdjust;

			sb.Append(FormatItemN(Column, "CDModelFile", CDModelFile ?? ""));
			sb.Append(FormatItemN(Column, "LibraryModelFile", LibraryModelFile ?? ""));
			sb.Append(FormatItemN(Column, "KeynoteFile", KeynoteFile ?? ""));
			sb.Append(FormatItemN(Column, "LinkedFolder", LinkedFolder ?? ""));
			sb.Append(FormatItemN(Column, "XrefFolder", XrefFolder ?? ""));

			Column -= ColumnAdjust;

			return sb.ToString();
		}
	}


//------------------------------------------------------------------------
	// holds project information
	public partial class ProjectDataProject
	{
		internal ProjectDataProject() { }

		internal ProjectDataProject(ProjData pData)
		{
			if (pData?.Project == null) return;

			if (NumberIsNullOrEmpty(pData.Project[PROJ]))
				ID = pData.Project[PROJ].ID;

			if (DescriptionIsNullOrEmpty(pData.Project[PROJ]))
				ID = pData.Project[PROJ].ID;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemN(Column, "Root Project Information"));

			Column += ColumnAdjust;

			sb.Append(FormatItemN(Column, "Project Number", ID));
			sb.Append(FormatItemN(Column, "Description", Description));
			sb.Append(FormatItemN(Column, "Root Folder", RootFolder));

			Column -= ColumnAdjust;

			return sb.ToString();
		}
	}

//------------------------------------------------------------------------
	// holds information about information found
	public struct FindItem
	{
		internal IDInfo Item;
		internal List<FindItem> FoundItems;

		public FindItem(IDInfo item, List<FindItem> founditems)
		{
			Item = item;
			FoundItems = founditems;
		}
	}

//------------------------------------------------------------------------
	// 
	public struct ChangeItem : IComparable<ChangeItem>
	{
		public string TskPhBldg;
		public UserProj uProjExisting;
		public UserProj uProjNew;

		public ChangeItem(UserProj uProjExisting, UserProj uProjNew)
		{
			this.uProjExisting = uProjExisting;
			this.uProjNew = uProjNew;
			TskPhBldg = Util.FormatTaskPhaseBuilding(uProjExisting);
		}

		public int CompareTo(ChangeItem compareItem)
		{
			return TskPhBldg.CompareTo(compareItem.TskPhBldg);
		}

	}

	
}