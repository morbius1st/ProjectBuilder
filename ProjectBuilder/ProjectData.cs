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
	 * ✔ AddProject(uProjectData) => bool
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


	public partial class ProjectData : IProjDataB
	{
		private string xmlFile;
		private bool configured = false;

		[XmlIgnore] 
		public override string ItemID => Project.ID;

		[XmlIgnore]
		public override string ItemDescription => Project.Description;

		[XmlIgnore]
		public override List<ProjectDataTask> ItemList => Tasks;

		public string XmlFile => xmlFile;

		public bool Configured => configured;

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

		public void Save()
		{
			SaveToFile(XmlFile);
		}

		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}

	}

//	public partial class ProjectData : IProjDataB
//	{
//		[XmlIgnore]
//		public override string ItemID => "";
//
//		[XmlIgnore]
//		public override string ItemDescription => "";
//
//		[XmlIgnore]
//		public override List<ProjectDataTask> ItemList => new List<ProjectDataTask>();
//
//
//
//
//
//		// todo: implement code
//		public bool Update(ProjData pData)
//		{
//			return true;
//		}
//	}

	public partial class ProjectDataTask : IProjDataB
	{
		[XmlIgnore]
		public override string ItemID => "";

		[XmlIgnore]
		public override string ItemDescription => "";

		[XmlIgnore]
		public override List<ProjectDataTaskPhase> ItemList => new List<ProjectDataTaskPhase>();




		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}
	}

	public partial class ProjectDataTaskPhase : IProjDataB
	{
		[XmlIgnore]
		public override string ItemID => "";

		[XmlIgnore]
		public override string ItemDescription => "";

		[XmlIgnore]
		public override List<ProjectDataTaskPhaseBldg> ItemList => new List<ProjectDataTaskPhaseBldg>();




		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}
	}

	public partial class ProjectDataTaskPhaseBldg : IProjDataB
	{
		// parameterless constructor is required
		internal ProjectDataTaskPhaseBldg() { }

		internal ProjectDataTaskPhaseBldg(string id, string description)
		{
			ID = id ?? "";
			Description = description ?? "";
		}

		// UserProj
		// ProjData
		internal ProjectDataTaskPhaseBldg(UserProj uProj) : this(uProj.BldgKey.ID, uProj.BldgKey.Description)
		{
			CDFolder = "";
			SheetNumberFormat = "";
			Location = new ProjectDataTaskPhaseBldgLocation();
		}

		[XmlIgnore]
		public override string ItemID => ID;

		[XmlIgnore]
		public override string ItemDescription => Description;

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

			sb.Append(FormatItemN("Building", ID));
			sb.Append(FormatItemN("Description", Description));

			return sb.ToString();
		}
	}

	public partial class ProjectDataTaskPhaseBldgLocation : IProjDataA
	{


		// todo: implement code
		public bool Update(ProjData pData)
		{
			return true;
		}
	}

	public partial class ProjectDataTaskPhaseBldgLocationAutoCAD : IProjDataA
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
	public partial class ProjectDataTaskPhaseBldgLocationRevit : IProjDataA
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