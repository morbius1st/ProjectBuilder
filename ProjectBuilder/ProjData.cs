
namespace ProjectBuilder
{
	public class ProjData
	{
		public UserProj Project;
		public string RootFolder;
		public string CDFolder;
		public ShtNumFmt SheetNumberFormat;
		public ProjDataAutoCAD AutoCAD;
		public ProjDataRevit Revit;


		public ProjData() { }

		public ProjData(UserProj uProj, string rootFolder) :
			this(uProj, rootFolder, null, null, new ProjDataAutoCAD(), new ProjDataRevit())
		{ }

		public ProjData(UserProj uProj, string rootFolder, string cdFolder,
			ShtNumFmt shtnumfmt, ProjDataAutoCAD autoCad, ProjDataRevit revit)
		{
			Project = uProj;
			SheetNumberFormat = shtnumfmt;
			RootFolder = rootFolder;
			CDFolder = cdFolder;
			AutoCAD = autoCad;
			Revit = revit;

		}

		public ProjData Clone()
		{
			return new ProjData(Project.Clone(), RootFolder,
				CDFolder, SheetNumberFormat, AutoCAD.Clone(), Revit.Clone());
		}

		// update project number information
		public void Update(UserProj uProj)
		{
			if (!IDInfo.NumberIsNullOrEmpty(uProj.TaskKey)) Project.TaskKey.ID = uProj.TaskKey.ID;
			if (!IDInfo.DescriptionIsNullOrEmpty(uProj.TaskKey)) Project.TaskKey.Description = uProj.TaskKey.Description;

			if (!IDInfo.NumberIsNullOrEmpty(uProj.PhaseKey)) Project.PhaseKey.ID = uProj.PhaseKey.ID;
			if (!IDInfo.DescriptionIsNullOrEmpty(uProj.PhaseKey)) Project.PhaseKey.Description = uProj.PhaseKey.Description;

			if (!IDInfo.NumberIsNullOrEmpty(uProj.BldgKey)) Project.BldgKey.ID = uProj.BldgKey.ID;
			if (!IDInfo.DescriptionIsNullOrEmpty(uProj.BldgKey)) Project.BldgKey.Description = uProj.BldgKey.Description;
		}

		public void UpdateDescriptions(UserProj uProj)
		{
			if (!IDInfo.DescriptionIsNullOrEmpty(uProj.TaskKey)) Project.TaskKey.Description = uProj.TaskKey.Description;

			if (!IDInfo.DescriptionIsNullOrEmpty(uProj.PhaseKey)) Project.PhaseKey.Description = uProj.PhaseKey.Description;

			if (!IDInfo.DescriptionIsNullOrEmpty(uProj.BldgKey)) Project.BldgKey.Description = uProj.BldgKey.Description;
		}
	}

	public class ProjDataAutoCAD
	{
		public string SheetFolder;
		public string XrefFolder;
		public string DetailFolder;
		public string BorderFile;

		public ProjDataAutoCAD()
		{
			SheetFolder = null;
			XrefFolder = null;
			DetailFolder = null;
			BorderFile = null;
		}

		public ProjDataAutoCAD(string sFolder,
			string xFolder, string dFolder, string bFolder)
		{
			SheetFolder = sFolder;
			XrefFolder = xFolder;
			DetailFolder = dFolder;
			BorderFile = bFolder;
		}

		public ProjDataAutoCAD Clone()
		{
			return new ProjDataAutoCAD(SheetFolder,
				XrefFolder, DetailFolder, BorderFile);
		}

	}

	public class ProjDataRevit
	{
		public string CDModelFile;
		public string LibraryModelFile;
		public string KeynoteFile;
		public string LinkedFolder;
		public string XrefFolder;

		public ProjDataRevit()
		{
			CDModelFile = null;
			LibraryModelFile = null;
			KeynoteFile = null;
			LinkedFolder = null;
			XrefFolder = null;
		}

		public ProjDataRevit(string cFile,
			string lFile, string kFile, string lFolder, string xFolder)
		{
			CDModelFile = cFile;
			LibraryModelFile = lFile;
			KeynoteFile = kFile;
			LinkedFolder = lFolder;
			XrefFolder = xFolder;
		}

		public ProjDataRevit Clone()
		{
			return new ProjDataRevit(CDModelFile,
					LibraryModelFile, KeynoteFile,
					LinkedFolder, XrefFolder);
		}
	}
}