using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;
using System.Xml.XPath;

using static ProjectBuilder.Util;

namespace ProjectBuilder
{
	public class TestProjectData
	{
		private const string PROJECT_FILE = @"C:\2099-999 Sample Project\CD\.config";
		private const string XML_FILE_NAME = "8998 ProjectData.xml";
		private static readonly string XmlProjectFile = string.Concat(PROJECT_FILE, "\\", XML_FILE_NAME);

		private const string XML_TEST_FILE_NAME = "xxxx ProjectData.xml";
		private static readonly string XmlTestProjectFile = string.Concat(PROJECT_FILE, "\\", XML_TEST_FILE_NAME);

		List<ChangeItem> changeList = new List<ChangeItem>();

		internal TestProjectData(ProjectBuilderForm form)
		{
			form.SetMessageText("This is message text from ProjectData");
			form.SetFieldText(ProjectDataTests().ToString());
		}

		private StringBuilder ProjectDataTests()
		{
			StringBuilder sb = new StringBuilder();

			ProjData pDataNew = AssignProjDataNew();

			ProjectData pd = ProjectData.LoadFile(XmlProjectFile);

			List<UserProj> upList;


			// existing @both
			UserProj upAll = new UserProj(null, new IDInfo(All),
				new IDInfo(All), new IDInfo(All));

			UserProj up_00_all_all = new UserProj(null, new IDInfo("00"),
				new IDInfo(All), new IDInfo(All));

			UserProj up_all_1_all = new UserProj(null, new IDInfo(All),
				new IDInfo("1"), new IDInfo(All));

			UserProj up_mt_mt_mt = new UserProj(null, new IDInfo(""),
				new IDInfo(""), new IDInfo(""));

			UserProj up_null_null_null = new UserProj();



			// existing @8999 (8998)
			UserProj up_02_1_all = new UserProj(null, new IDInfo("02"),
				new IDInfo("1"), new IDInfo(All));

			UserProj up_02_1_C = new UserProj(null, new IDInfo("02"),
				new IDInfo("1"), new IDInfo("C"));

			UserProj up_02_1_B = new UserProj(null, new IDInfo("02"),
				new IDInfo("1"), new IDInfo("B"));


			IDInfo px = new IDInfo("2099-999", "TestX");
			// existing @xxxx
			UserProj up_00_all_B = new UserProj(px, new IDInfo("00"),
				new IDInfo(All), new IDInfo("B"));

			UserProj up_02_5_G = new UserProj(px, new IDInfo("02", "Task 02"),
				new IDInfo("5", "Phase 05"), new IDInfo("G", "Building G"));

			UserProj up_02_5_H = new UserProj(px, new IDInfo("02", "Task 02"),
				new IDInfo("5", "Phase 05"), new IDInfo("H", "Building H"));

			UserProj up_00_1_F = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("1", "Phase 01"), new IDInfo("F", "Building F"));

			UserProj up_00_6_F = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("6", "Phase F"), new IDInfo("F", "Building F"));

			UserProj up_00_mt_A = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("", "Phase none"), new IDInfo("A", "Building A"));

			UserProj up_00_mt_B = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("", "Phase none"), new IDInfo("B", "Building B"));

			UserProj up_01_mt_mt = new UserProj(px, new IDInfo("01", "Task 01"),
				new IDInfo("", "No Phase"), new IDInfo("", "No Building"));


			// new
			UserProj up_00_mt_C = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("", "Phase none"), new IDInfo("C", "Building C"));

			UserProj up_01_1_mt = new UserProj(px, new IDInfo("01"),
				new IDInfo("1"), new IDInfo(""));

			UserProj up_00_2_C = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("2", "Phase 2"), new IDInfo("C", "Building C"));

			UserProj up_00_2_D = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("2", "Phase 2"), new IDInfo("D", "Building D"));

			UserProj up_03_0_C = new UserProj(px, new IDInfo("03", "Task 03"),
				new IDInfo("0", "Phase 0"), new IDInfo("C", "Building C"));

			UserProj up_00_mt_A_desc = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("", "Phase does not apply"), new IDInfo("A", "Building A"));

			UserProj up_00_mt_B_desc = new UserProj(px, new IDInfo("00", "Task 00"),
				new IDInfo("", "Phase does not apply"), new IDInfo("B", "Building B"));


			ProjData upd01;
			ProjData upd02;

			ProjectDataTaskPhaseBldg pBldg;

			List<UserProj> upListExist01;
			List<UserProj> upListNew01;



			string[] tests = new[] { "101" };   // default


			bool[] programsBoth = { true, true };
			bool[] programsAcad = { true, false };
			bool[] programsRevit = { false, true };

			bool[] programs = programsBoth;




			//			tests = new[] { "0", "X", "41" };		// test creating a file

			//			tests = new[] { "D", "2", "3", "D" };	// xml file information (must be loaded first)

//						tests = new[] { "41"};					// display whole file

//			tests = new[] { "51" };			// list specific task
//			tests = new[] { "52" };			// list specific task & phase

			//			tests = new[] { "61", "D" };			// list specific phase

//			tests = new[] { "101" };				// find all (standard)

//			tests = new[] { "102" };				// find specific task

//			tests = new[] { "103" };				// alt find all projects

//			tests = new[] { "104" };				// alt find

//			tests = new[] { "105" };				// alt find

//			tests = new[] { "106" };				// alt find all mt

//			tests = new[] { "107" };				// alt find all null

//			tests = new[] { "111"};					// find specific task / phase

//			tests = new[] { "121" };				// find specific task / Bldg

//			tests = new[] { "122" };				// find with empty value

			//			tests = new[] { "X", "122", "101" };	// find with empty value

//			tests = new[] { "151"};					// exists: all - false

//			tests = new[] { "152"};					// exists: specific task - false

//			tests = new[] { "153" };				// exists: specific task / phase / bldg

			//			tests = new[] { "151", "152", "161" };	// 

//			tests = new[] { "201" };				// get project information

//			tests = new[] {"211"};					// get sheet number format: non-specific

//			tests = new[] { "212" };                // get sheet number format: specific task / phase / bldg

//			tests = new[] { "213" };                // get sheet number format: specific task / phase / bldg

//			tests = new[] { "221" };            // get ACAD Location: non-specific task / phase / bldg

//			tests = new[] { "222" };            // get ACAD Location: specific task / phase / bldg

//			tests = new[] { "231" };			// get Revit Location: non-specific task / phase / bldg

//			tests = new[] { "232" };			// get Revit Location: specific task / phase / bldg

			tests = new[] { "101", "301", "101" };			// add a new project (task, phase, & building)

			//			tests = new[] { "X", "101", "320", "101", 
			//				"E", "101" };	// load test file and delete one project

			//			tests = new[] { "X", "101", "321", "101", "322", "101" };	// load test file and delete one project

			//			tests = new[] { "X", "101", "323", "101" };		// delete one

			//			tests = new[] { "X", "101", "307", "101", "323", "101" };	// change test - add then delete

			//			tests = new[] { "X", "101", "351", "101" };		// create a change list

			//			tests = new[] { "X", "101", "351", "352", "101" };

			// test: create whole new project
			//			tests = new[] { "0", "X", "301", "302", "303", "304", "305", "306", "S", "41" };

			// copy project
			//			tests = new[] {"X", "41", "330", "41"};

			// copy project data
			//			tests = new[] { "X", "331"};

			// change some projects number(s)
			//			tests = new[] { "X", "101", "351", "361", "101" };

			// change some project desciption(s)
			//			tests = new[] { "X", "101", "352", "361", "101" };

			// change some projects, save, load, display
			//			tests = new[] { "X", "101", "351", "361", "S", "E", "101", "X", "101"};

			// change acad location info
			//			tests = new[] {"371"};

			// change revit location info
			//			tests = new[] { "372" };

			// test the generic method
			//			tests = new[] { "junk" };

			//			// sort the data using the x-file and save
			//			tests = new[] { "X", "101", "380", "101", "S" };

			// sort the data using the primary file
			//			tests = new[] { "101", "380", "101" };

			// add location information in the x-file
			//			tests = new[] { "X", "390" };

			// general change project information
			//			tests = new[] { "X", "400" };




			// begin testing
			sb.Append(FormatItemDivider());
			sb.Append(FormatItemN("running tests:", ""));

			int i = 1;

			foreach (string test in tests)
			{
				sb.Append(test);

				if (i++ != tests.Length)
					sb.Append(" :: ");
			}

			sb.Append(FormatItemDividerN());

			StringBuilder sbTemp = new StringBuilder();
			UserProj up01;
			UserProj up02;

			foreach (string test in tests)
			{
				switch (test)
				{
					case "E":   // load the base example file
						pd = ProjectData.LoadFile(XmlProjectFile);
						break;

//					case "D":   // divider
//						sb.Append(FormatItemDividerN());
//						break;

					case "S":
						pd.Save();
						sb.Append(FormatItemN("saving file to", pd.XmlFile));
						break;

					case "X":   // load the alternate example file
						sb.Append(FormatItemN("loading file", XmlTestProjectFile));
						pd = ProjectData.LoadFile(XmlTestProjectFile);
						break;

					case "0":   // create new file
						FileInfo F = new FileInfo(XmlTestProjectFile);
						ProjectData pdNew = ProjectData.CreateFile(XmlTestProjectFile, pDataNew);
						sb.Append(FormatItemN("File Created?", (F.Length != 0).ToString()));
						break;

					case "2":   // show loaded file xml file name
						sb.Append(FormatItem("xmlFile", pd.XmlFile));
						break;

					case "3":   // show if project data is configured
						sb.Append(FormatItem("configured", pd.Configured.ToString()));
						break;

//					case "11":  // list the project information
//						ProjectDataProjectInformation pi = pd.ProjectInformation;
//						sb.Append(pi.ToString());
//						break;
//					case "21":  // list progm
//						sb.Append(ListProgm());
//						break;
//					case "22":  // list the sheet number formats
//						sb.Append(ListShtNumFmt());
//						break;
//					//					case "31":	// list an autocad project data
//					//						sb.Append(uPDAcad.ToString());
//					//						break;
//					//					case "32":	// list a revit project data
//					//						sb.Append(uPDRevit.ToString());
//					//						break;
					case "41":  // list all project information 
						sb.Append(ListProjectData(upAll, pd));
						break;
					case "51":  // list project information for a task
						sb.Append(ListProjectData(up_00_all_all, pd));
						break;
					case "52":	// list project information for a task
						sb.Append(ListProjectData(up_02_1_all, pd));
						break;
//					//					case "61":	// list project information for a task
//					//						sb.Append(pd.ListProjectData(up_all_1_all, programs));
//					//						break;
//					//					case "91":	// list project information for a task
//					//						sb.Append(pd.ListProjectData(up_02_1_C, programs));
//					//						break;
					case "101": // find projects: all
						up01 = upAll;
						sb.Append(FormatItemN("data file", pd.XmlFile));
						sb.Append(FormatItemN("finding", FormatProject(up01).ToString()));
						upList = pd.Find(up01);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "102": // find projects: per task
						upList = pd.Find(up_00_all_all);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "103": // alt find projects: all
						up01 = upAll;
						sb.Append(FormatItemN("data file", pd.XmlFile));
						sb.Append(FormatItemN("finding", FormatProject(up01).ToString()));
						upList = pd.Find(up01);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "104": // find projects: specific task / phase / bldg
						upList = pd.Find(up_02_1_C);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "105": // find projects: specific task
						upList = pd.Find(up_00_all_all);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "106": // find projects: specific task
						upList = pd.Find(up_mt_mt_mt);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "107": // find projects: specific task
						upList = pd.Find(up_null_null_null);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;

					case "111":
						up01 = up_02_1_all;
						sb.Append(FormatItemN("finding", FormatProject(up01).ToString()));
						upList = pd.Find(up01);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "121":
						upList = pd.Find(up_00_all_B);
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "122":
						up01 = up_00_mt_A;
						upList = pd.Find(up01);
						sb.Append(FormatItemN("finding", FormatProject(up01).ToString()));
						sb.Append(FormatItemN("found items", upList.Count.ToString()));
						sb.Append(FormatProjectList(upList));
						break;
					case "151":
						sb.Append(FormatItemN("this project", ":"));
						sb.Append(FormatProject(upAll));
						sb.Append(FormatItemN("found?", pd.Exists(upAll).ToString()));
						break;
					case "152":
						sb.Append(FormatItemN("this project", ":"));
						sb.Append(FormatProject(up_00_all_all));
						sb.Append(FormatItemN("found?", pd.Exists(up_00_all_all).ToString()));
						break;
					case "153":
						sb.Append(FormatItemN("this project", ":"));
						sb.Append(FormatProject(up_02_1_C));
						sb.Append(FormatItemN("found?", pd.Exists(up_02_1_C).ToString()));
						break;
					case "201":
						sb.Append(FormatProjectInfo(pd.GetProject()));
						break;
					case "211": // fail - no specific project found
						up01 = upAll;

						sbTemp.Append("Sht num fmt for: ").Append(FormatProject(up01));

						sb.Append(FormatItemN(sbTemp.ToString(), ":"));

						pBldg = pd.GetBuilding(up01);

						sb.Append(pBldg?.SheetNumberFormat ?? "is null");
						break;
					case "212": // normal find an existing project
						up01 = up_02_1_C;

						sbTemp.Append("Sht num fmt for: ").Append(FormatProject(up01));

						sb.Append(FormatItemN(sbTemp.ToString(), ":"));

						pBldg = pd.GetBuilding(up01);

						sb.Append(pBldg?.SheetNumberFormat ?? "is null");
						break;
					case "213": // special - project found but no sheet number format provided
						up01 = up_02_1_B;

						sbTemp.Append("Sht num fmt for: ").Append(FormatProject(up01));

						sb.Append(FormatItemN(sbTemp.ToString(), ":"));

						pBldg = pd.GetBuilding(up01);

						sb.Append(pBldg?.SheetNumberFormat ?? "is null");

						if (pBldg.SheetNumberFormat == null)
						{
							sb.Append("number format is null");
						}
						else if (pBldg.SheetNumberFormat.Equals(""))
						{
							sb.Append("number format is empty");  // correct answer
						}

						break;
					case "221":
						up01 = upAll;
						sbTemp.Append("ACAD Loc for: ").Append(FormatProject(up01));
						sb.Append(FormatItemN(sbTemp.ToString(), ""));
					
						sb.Append(FormatACADLocationInfo(pd.GetBuilding(up01)?.Location.AutoCAD));
						break;
					
					case "222":
						up01 = up_02_1_B;
						sbTemp.Append("ACAD Loc for: ").Append(FormatProject(up01));
						sb.Append(FormatItemN(sbTemp.ToString(), ""));
					
						sb.Append(FormatACADLocationInfo(pd.GetBuilding(up01)?.Location.AutoCAD));
						break;
					
					case "231":
						up01 = upAll;
						sbTemp.Append("Revit Loc for: ").Append(FormatProject(up01));
						sb.Append(FormatItemN(sbTemp.ToString(), ""));
					
						sb.Append(FormatRevitLocationInfo(pd.GetBuilding(up01)?.Location.Revit));
						break;
					
					case "232":
						up01 = up_00_6_F;
						sbTemp.Append("Revit Loc for: ").Append(FormatProject(up01));
						sb.Append(FormatItemN(sbTemp.ToString(), ""));
					
						sb.Append(FormatRevitLocationInfo(pd.GetBuilding(up01)?.Location.Revit));
						break;
					
					case "301":
						up01 = up_02_5_G;
						sb.Append(CreateNewProject(pd, up01));
						break;
					
					case "302":
						up01 = up_02_5_H;
						sb.Append(CreateNewProject(pd, up01));
						break;
					
					case "303":
						up01 = up_00_1_F;
						sb.Append(CreateNewProject(pd, up01));
						break;
					
					case "304":
						up01 = up_00_mt_A;
						sb.Append(CreateNewProject(pd, up01));
						break;
					
					case "305":
						up01 = up_00_mt_B;
						sb.Append(CreateNewProject(pd, up01));
						break;
					
					case "306":
						up01 = up_01_mt_mt;
						sb.Append(CreateNewProject(pd, up01));
						break;
					
					case "307":
						up01 = up_00_mt_C;
						sb.Append(CreateNewProject(pd, up01));
						break;
					//
					//					case "320":
					//						up01 = up_02_5_H;
					//						sb.Append(FormatItemN("Deleting project", FormatProject(up01).ToString()));
					//						sb.Append(FormatItemN("deleted?", pd.DeleteProject(up01).ToString()));
					//						break;
					//
					//					case "321":
					//						up01 = up_01_1_mt;
					//						sb.Append(FormatItemN("Deleting project", FormatProject(up01).ToString()));
					//						sb.Append(FormatItemN("deleted?", pd.DeleteProject(up01).ToString()));
					//						break;
					//
					//					case "322":
					//						up01 = up_01_mt_mt;
					//						sb.Append(FormatItemN("Deleting project", FormatProject(up01).ToString()));
					//						sb.Append(FormatItemN("deleted?", pd.DeleteProject(up01).ToString()));
					//						break;
					//
					//					case "323":
					//						up01 = up_00_mt_A;
					//						sb.Append(FormatItemN("Deleting project", FormatProject(up01).ToString()));
					//						sb.Append(FormatItemN("deleted?", pd.DeleteProject(up01).ToString()));
					//						break;
					//
					//					case "330":     // copy project information & change
					//						up01 = up_02_5_G;
					//						up02 = up_03_0_C;
					//						upd02 = pd.Copy(up01);
					//
					//						// got copy - change names
					//						upd02.Project.TaskKey = up02.TaskKey.Clone();
					//						upd02.Project.PhaseKey = up02.PhaseKey.Clone();
					//						upd02.Project.BldgKey = up02.BldgKey.Clone();
					//
					//						// add the revision as a new project
					//						pd.Add(upd02);
					//
					//						// delete the old project
					//						pd.DeleteProject(up01);
					//
					//						break;
					//
					//					case "331": // copy test
					//						up01 = up_02_5_G;
					//						upd02 = pd.Copy(up01);
					//
					//						sb.Append(FormatItemN("original project", FormatProject(up01).ToString()));
					//						sb.Append(FormatItemN("copy project", FormatProject(upd02).ToString()));
					//
					//						break;
					//
					//
					//					// create a change list
					//					case "351":
					//						upListExist01 = new List<UserProj> { up_00_mt_A, up_00_mt_B };
					//						upListNew01 = new List<UserProj> { up_00_2_C, up_00_2_D };
					//
					//						sb.Append(FormatChangeList(upListExist01, upListNew01, pd));
					//
					//						break;
					//
					//					// create a change list
					//					case "352":
					//						upListExist01 = new List<UserProj> { up_00_mt_A, up_00_mt_B };
					//						upListNew01 = new List<UserProj> { up_00_mt_A_desc, up_00_mt_B_desc };
					//
					//						sb.Append(FormatChangeList(upListExist01, upListNew01, pd));
					//
					//						break;
					//
					//					// change projects based on the change lists
					//					case "361":
					//						int[] Failed = new int[changeList.Count];
					//
					//						sb.Append(FormatItemN("change worked?", pd.ChangeProject(changeList, out Failed).ToString()));
					//
					//						sb.Append(FormatItemN("Success / fails", ""));
					//
					//						foreach (int fail in Failed)
					//						{
					//							sb.Append(fail.ToString()).Append("  ");
					//						}
					//
					//
					//						break;
					//
					//					// change AutoCAD location Information
					//					case "371":
					//						up01 = up_00_6_F;
					//
					//						upd01 = MakeProjectEx(up01);
					//
					//						sb.Append(nl).Append("project data input");
					//						sb.Append(FormatProject(upd01));
					//
					//						sb.Append(FormatItemDividerN());
					//
					//						sb.Append(FormatItemN("change worked?",
					//							pd.ChangeAcadLocation(upd01).ToString()));
					//
					//						sb.Append(nl).Append("project data changed");
					//						sb.Append(ListProjectData(up01, pd));
					//						break;
					//
					//					// change Revit location Information
					//					case "372":
					//						up01 = up_00_6_F;
					//
					//						upd01 = MakeProjectEx(up01);
					//
					//						sb.Append(nl).Append("project data input");
					//						sb.Append(FormatProject(upd01));
					//
					//						sb.Append(FormatItemDividerN());
					//
					//						sb.Append(FormatItemN("change worked?",
					//							pd.ChangeRevitLocation(upd01).ToString()));
					//
					//						sb.Append(nl).Append("project data changed");
					//						sb.Append(ListProjectData(up01, pd));
					//						break;
					//
					//					case "380":
					//						sb.Append(nl).Append("sorting the data");
					//						pd.Sort();
					//						break;
					//
					//					// add autocad location via change autocad location information
					//					case "390":
					//						up01 = up_00_mt_A;
					//
					//						sb.Append(ListProjectData(up01, pd));
					//
					//						sb.Append(FormatItemDividerN());
					//
					//						upd01 = MakeProjectEx(up01);
					//
					//						sb.Append(nl).Append("project data input");
					//						sb.Append(FormatProject(upd01));
					//
					//						sb.Append(FormatItemDividerN());
					//
					//						sb.Append(FormatItemN("change worked?",
					//							pd.ChangeAcadLocation(upd01).ToString()));
					//
					//						sb.Append(nl).Append("project data changed");
					//						sb.Append(ListProjectData(up01, pd));
					//
					//						break;
					//
					//					// general change project data
					//					case "400":
					//						up01 = up_00_mt_A;
					//						up02 = new UserProj();
					//						up02.TaskKey = new IDInfo("99");
					//
					//						pDataNew = MakeProjectEx(up02);
					//
					//						sb.Append(FormatItemN("changing project data", ""));
					//
					//						sb.Append(ListProjectData(up01, pd));
					//
					//						sb.Append(FormatItemN("changing project data result",
					//							pd.ChangeProjectInfo(up01, pDataNew).ToString()));
					//
					//						break;

					default:
						sb.Append("no tests selected");
						break;
				}

				sb.Append(FormatItemDividerN());
				sbTemp.Clear();
			}

			return sb;
		}

		internal StringBuilder FormatChangeList(List<ChangeItem> changeList)
		{
			StringBuilder sb = new StringBuilder();

			foreach (ChangeItem item in changeList)
			{
				sb.Append(FormatItemN("tskphbld", item.TskPhBldg));
				sb.Append(FormatItemN(4, "existing", FormatProject(item.uProjExisting).ToString()));
				sb.Append(FormatItemN(4, "new", FormatProject(item.uProjNew).ToString()));
			}
			return sb;
		}

		internal StringBuilder FormatProjectList(List<UserProj> upList)
		{
			StringBuilder sb = new StringBuilder();

			int i = 1;

			foreach (UserProj uProj in upList)
			{
				sb.Append(FormatItemN("project number", (i++.ToString("00: ") + FormatProjectEx(uProj).ToString())));
			}
			return sb;
		}

		internal StringBuilder FormatProject(ProjData pData)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemN("Project number", FormatProjectExx(pData.Project).ToString()));

			sb.Append(FormatItemN(2, "roof folder", pData.RootFolder));
			sb.Append(FormatItemN(2, "cd folder", pData.CDFolder));
			sb.Append(FormatItemN(2, "sht number format", pData.SheetNumberFormat.Name +
				" : " + pData.SheetNumberFormat.Format));

			sb.Append(FormatItemN(2, "AutoCAD info"));

			sb.Append(FormatItemN(4, "sht folder", pData.AutoCAD.SheetFolder));
			sb.Append(FormatItemN(4, "xref folder", pData.AutoCAD.XrefFolder));
			sb.Append(FormatItemN(4, "detail folder", pData.AutoCAD.DetailFolder));
			sb.Append(FormatItemN(4, "border file", pData.AutoCAD.BorderFile));

			sb.Append(FormatItemN(2, "Revit info"));
			sb.Append(FormatItemN(4, "model file", pData.Revit.CDModelFile));
			sb.Append(FormatItemN(4, "library file", pData.Revit.LibraryModelFile));
			sb.Append(FormatItemN(4, "keynote file", pData.Revit.KeynoteFile));
			sb.Append(FormatItemN(4, "linked folder", pData.Revit.LinkedFolder));
			sb.Append(FormatItemN(4, "xref folder", pData.Revit.XrefFolder));

			return sb;
		}


		internal StringBuilder FormatProject(UserProj uProj)
		{
			StringBuilder projNumber = new StringBuilder();

			string tsk = (uProj.TaskKey.ID ?? "null").Equals("") ? "<mt>" : uProj.TaskKey.ID;
			string ph = (uProj.PhaseKey.ID ?? "null").Equals("") ? "<mt>" : uProj.PhaseKey.ID;
			string bld = (uProj.BldgKey.ID ?? "null").Equals("") ? "<mt>" : uProj.BldgKey.ID;

			projNumber.Append("tsk: ").Append(tsk);
			projNumber.Append(" ph: ").Append(ph);
			projNumber.Append(" bld: ").Append(bld);

			return projNumber;
		}

		internal StringBuilder FormatProjectEx(UserProj uProj)
		{
			StringBuilder projNumber = new StringBuilder();

			string tsk = (uProj.TaskKey.ID ?? "null").Equals("") ? "<mt>" : uProj.TaskKey.ID;
			string ph = (uProj.PhaseKey.ID ?? "null").Equals("") ? "<mt>" : uProj.PhaseKey.ID;
			string bld = (uProj.BldgKey.ID ?? "null").Equals("") ? "<mt>" : uProj.BldgKey.ID;

			string tsk_desc = (uProj.TaskKey.Description ?? "null").Equals("") ? "<mt>" : uProj.TaskKey.Description;
			string ph_desc = (uProj.PhaseKey.Description ?? "null").Equals("") ? "<mt>" : uProj.PhaseKey.Description;
			string bld_desc = (uProj.BldgKey.Description ?? "null").Equals("") ? "<mt>" : uProj.BldgKey.Description;

			projNumber.Append(tsk).Append(" - ").Append(tsk_desc).Append(" | ");
			projNumber.Append(ph).Append(" - ").Append(ph_desc).Append(" | ");
			projNumber.Append(bld).Append(" - ").Append(bld_desc);

			return projNumber;
		}

		internal StringBuilder FormatProjectExx(UserProj uProj)
		{
			StringBuilder projNumber = new StringBuilder();

			string proj = (uProj.ProjKey.ID ?? "null").Equals("") ?
				"<mt>" : uProj.ProjKey.ID;
			string proj_desc = (uProj.ProjKey.Description ?? "null").Equals("") ?
				"<mt>" : uProj.ProjKey.Description;

			projNumber.Append(proj).Append(" - ").Append(proj_desc).Append(" | ");

			projNumber.Append(FormatProjectEx(uProj));

			return projNumber;
		}

		internal StringBuilder
			FormatProjectInfo(ProjectDataProject pdInfo)
		{
			StringBuilder sb = new StringBuilder();

			if (pdInfo == null)
				return sb.Append(FormatItemN("pdInfo", "is null"));

			sb.Append(FormatItemN("Project number", pdInfo.ID));
			sb.Append(FormatItemN("Project description", pdInfo.Description));
			sb.Append(FormatItemN("Root folder", pdInfo.RootFolder));

			return sb;
		}

		internal StringBuilder
			FormatACADLocationInfo(ProjectDataTaskPhaseBldgLocationAutoCAD acadLoc)
		{
			StringBuilder sb = new StringBuilder();

			if (acadLoc == null)
				return sb.Append(FormatItemN("Acad Location", "is null"));

			sb.Append(FormatItemN("SheetFolder", acadLoc.SheetFolder));
			sb.Append(FormatItemN("XrefFolder", acadLoc.XrefFolder));
			sb.Append(FormatItemN("DetailFolder", acadLoc.DetailFolder));
			sb.Append(FormatItemN("BorderFile", acadLoc.BorderFile));

			return sb;
		}

		internal StringBuilder
			FormatRevitLocationInfo(ProjectDataTaskPhaseBldgLocationRevit revitLoc)
		{
			StringBuilder sb = new StringBuilder();

			if (revitLoc == null)
				return sb.Append(FormatItemN("Revit Location", "is null"));

			sb.Append(FormatItemN("CDModelFile", revitLoc.CDModelFile));
			sb.Append(FormatItemN("LibraryModelFile", revitLoc.LibraryModelFile));
			sb.Append(FormatItemN("LinkedFolder", revitLoc.LinkedFolder));
			sb.Append(FormatItemN("XrefFolder", revitLoc.XrefFolder));
			sb.Append(FormatItemN("KeynoteFile", revitLoc.KeynoteFile));

			return sb;
		}
//
//		internal StringBuilder FormatChangeList(List<UserProj> upListExist, List<UserProj> upListNew, ProjectData pd)
//		{
//			StringBuilder sb = new StringBuilder();
//
//			sb.Append(nl).Append("renaming projects");
//			sb.Append(nl).Append(nl).Append("existing project info");
//
//			sb.Append(FormatProjectList(upListExist));
//
//			sb.Append(nl).Append(nl).Append("new project info");
//			sb.Append(FormatProjectList(upListNew));
//
//			changeList = pd.MakeChangeList(upListExist, upListNew);
//			sb.Append(nl).Append(nl).Append("change list");
//			sb.Append(FormatChangeList(changeList));
//			return sb;
//
//		}
//
		private ProjData AssignProjDataNew()
		{
			UserProj uProj = new UserProj();

			uProj.ProjKey = new IDInfo("2099-999", "TextX");

			return new ProjData(uProj, @"p:\Root Folder");
		}


		private StringBuilder CreateNewProject(ProjectData pd, UserProj uProj)
		{
			StringBuilder sb = new StringBuilder();

			ProjData newProject = MakeProject(uProj);
			sb.Append(FormatItemN("Adding project", FormatProject(uProj).ToString()));
			sb.Append(FormatProject(newProject));
			sb.Append(FormatItemN("added?", pd.Add(newProject).ToString()));

			return sb;
		}

//		// create a revised project information data struct
//		private ProjData MakeProjectEx(UserProj uProj)
//		{
//			ProjData pData = new ProjData();
//
//			pData.Project = uProj.Clone();
//
//			string rootfolder = @"this is the root folder!";
//			string cdfolder = rootfolder + @"\some sub-folder";
//
//			pData.Valid = true;
//
//			pData.Project = uProj.Clone();
//			pData.SheetNumberFormat = ShtNumFmt.VOIDsnf;
//			pData.RootFolder = rootfolder;
//			pData.CDFolder = cdfolder;
//
//			pData.AutoCAD = MakeAutoCADEx();
//			pData.Revit = MakeRevitEx();
//
//			return pData;
//		}


		// create sample project information
		private ProjData MakeProject(UserProj uProj)
		{
			ProjData pData = new ProjData();

			string rootfolder = @"p:\" + uProj.ProjKey.ID + "-root";
			string cdfolder = rootfolder + @"\cd";

			pData.Project = uProj.Clone();
			pData.SheetNumberFormat = ShtNumFmt.LEVEL3;
			pData.RootFolder = rootfolder;
			pData.CDFolder = cdfolder;

			pData.AutoCAD = MakeAutoCAD(cdfolder, uProj);
			pData.Revit = MakeRevit(cdfolder, uProj);

			return pData;
		}

		private ProjDataAutoCAD MakeAutoCAD(string cdfolder, UserProj uProj)
		{
			ProjDataAutoCAD acad = new ProjDataAutoCAD();

			string tpb = uProj.TaskKey.ID + "-" + uProj.PhaseKey.ID + "-" + uProj.BldgKey.ID;

			string shtfolder = cdfolder + @"\" + tpb + "-acad";
			string detail = shtfolder + @"\detail";

			acad.SheetFolder = shtfolder;
			acad.XrefFolder = shtfolder + @"\x-ref";
			acad.DetailFolder = detail;
			acad.BorderFile = detail + @"\border.dwg";

			return acad;
		}

		private ProjDataAutoCAD MakeAutoCADEx()
		{
			ProjDataAutoCAD acad = new ProjDataAutoCAD();

			acad.SheetFolder = "this is the sheet folder";
			acad.XrefFolder = "this is the xref folder";
			acad.DetailFolder = "this is the detail folder";
			acad.BorderFile = "this is the border file";

			return acad;
		}

		private ProjDataRevit MakeRevit(string cdfolder, UserProj uProj)
		{
			ProjDataRevit revit = new ProjDataRevit();

			string tpb = uProj.TaskKey.ID + "-" + uProj.PhaseKey.ID + "-" + uProj.BldgKey.ID;

			string modelfolder = cdfolder + @"\" + tpb + "-revit";

			revit.CDModelFile = modelfolder + @"\model.rvt";
			revit.LibraryModelFile = modelfolder + @"\library.rvt";
			revit.KeynoteFile = modelfolder + @"\keynote.txt";
			revit.LinkedFolder = modelfolder + @"\Linked";
			revit.XrefFolder = modelfolder + @"\x-ref";

			return revit;
		}

		private ProjDataRevit MakeRevitEx()
		{
			ProjDataRevit revit = new ProjDataRevit();

			revit.CDModelFile = "this is the CD Model file";
			revit.LibraryModelFile = "this is the library file";
			revit.KeynoteFile = "this is the keynote file";
			revit.LinkedFolder = "this is the Linked folder";
			revit.XrefFolder = "this is the xref folder";

			return revit;
		}
//
//		private ProjDataRevit MakeRevitExx()
//		{
//			ProjDataRevit revit = new ProjDataRevit();
//
//			revit.CDModelFile = "this is the CD Model file";
//			revit.LibraryModelFile = "";
//			revit.KeynoteFile = "";
//			revit.LinkedFolder = "";
//			revit.XrefFolder = "";
//
//			return revit;
//		}

		private StringBuilder ListProjectData(UserProj uProj, ProjectData pd)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(FormatItemN("listing for project:"));

			sb.Append(uProj.ListProjectNumber());

			int column = Column = 0;

			sb.Append(FormatItemDividerN());
			sb.Append(FormatItemN(column, "Project Data"));
			sb.Append(FormatItemDividerN());

			sb.Append(pd.ToString());

			Column += ColumnAdjust;

			if (IDInfo.NumberIsAll(uProj.TaskKey))
			{
				// listing all tasks
				sb.Append(FormatItemDividerN());
				sb.Append(FormatItemN(column, "Tasks"));


				foreach (ProjectDataTask oneTask in pd.Tasks)
				{
					sb.Append(ListTask(uProj, oneTask));
					Column = column + ColumnAdjust;
				}
					
			}
			else
			{
				// listing one task
				ProjectDataTask oneTask = pd.FindTask(uProj);

				sb.Append(FormatItemDividerN());
				sb.Append(FormatItemN(column, "Tasks"));

				if (oneTask != null)
				{
					
					sb.Append(ListTask(uProj, oneTask));
				}
				else
				{
					sb.Append(FormatItemN(column, "Task: " + uProj.TaskKey.ID, "** not found **"));
				}
			}
			return sb;
		}

//		private StringBuilder ListTask(UserProj uProj, ProjectDataTask oneTask)
//		{
//			StringBuilder sb = new StringBuilder();
//
//			sb.Append(FormatItemN("Task ID", oneTask.ID));
//			sb.Append(FormatItemN("Task Name", oneTask.Description));
//
//			return sb;
//		}


		private StringBuilder ListTask(UserProj uProj, ProjectDataTask oneTask)
		{
			StringBuilder sb = new StringBuilder();

			int column = Column;

			sb.Append(FormatItemDividerN());
			sb.Append(oneTask.ToString());

			Column += ColumnAdjust;

			if (IDInfo.NumberIsAll(uProj.PhaseKey))
			{
				foreach (ProjectDataTaskPhase onePhase in oneTask.Phase)
				{
					sb.Append(ListPhase(uProj, onePhase));

					Column = column + ColumnAdjust;
				}
			}
			else
			{
				ProjectDataTaskPhase onePhase = oneTask.FindPhase(uProj);

				if (onePhase != null)
				{
					sb.Append(ListPhase(uProj, onePhase));
				}
				else
				{
					sb.Append(FormatItemN(column, "Phase: " + uProj.PhaseKey.ID, "** not found **"));
				}
			}
			return sb;
		}

		//		private StringBuilder ListPhase(UserProj uProj, ProjectDataTaskPhase onePhase)
		//		{
		//			StringBuilder sb = new StringBuilder();
		//
		//			sb.Append(onePhase.ToString());
		//
		//			return sb;
		//
		//
		//		}

		private StringBuilder ListPhase(UserProj uProj, ProjectDataTaskPhase onePhase)
		{
			StringBuilder sb = new StringBuilder();

			int column = Column;

			sb.Append(FormatItemDividerN());
			sb.Append(onePhase.ToString());

			Column += ColumnAdjust;

			if (IDInfo.NumberIsAll(uProj.BldgKey))
			{
				//				Debug.Print("listing all buildings");

				foreach (ProjectDataTaskPhaseBldg oneBldg in onePhase.Bldg)
				{
					sb.Append(ListBuilding(oneBldg));

					Column = column + ColumnAdjust;
				}
			}
			else
			{
				ProjectDataTaskPhaseBldg oneBldg = onePhase.FindBldg(uProj);

				if (oneBldg != null)
				{
					sb.Append(ListBuilding(oneBldg));
				}
				else
				{
					sb.Append(FormatItemN(column, "Building: " + uProj.BldgKey.ID, "** not found **"));
				}
			}

			return sb;
		}

//		private StringBuilder ListBuilding(ProjectDataTaskPhaseBldg oneBldg)
//		{
//			StringBuilder sb = new StringBuilder();
//
//			sb.Append(oneBldg.ToString());
//
//			return sb;
//		}



		private StringBuilder ListBuilding(ProjectDataTaskPhaseBldg oneBldg)
		{
			StringBuilder sb = new StringBuilder();
			int column = Column;


			sb.Append(FormatItemDividerN());
			sb.Append(oneBldg.ToString());

			Column += ColumnAdjust;

			if (oneBldg.Location != null)
			{
				if (oneBldg.Location.AutoCAD != null)
				{
					sb.Append(oneBldg.Location.AutoCAD.ToString());
				}
				else
				{
					sb.Append(FormatItemDividerN());
					sb.Append(FormatItemN(column + ColumnAdjust, "no autocad location information", ""));
				}

				if (oneBldg.Location.Revit != null)
				{
					sb.Append(oneBldg.Location.Revit.ToString());
				}
				else
				{
					sb.Append(FormatItemDividerN());
					sb.Append(FormatItemN(column + ColumnAdjust, "no revit location information", ""));
				}

			}
			else
			{
				sb.Append(FormatItemN(column + ColumnAdjust, "no location information", ""));
			}
			return sb;
		}

		//
		//		private StringBuilder ListShtNumFmt()
		//		{
		//			StringBuilder sb = new StringBuilder();
		//
		//			ShtNumFmt snf;
		//
		//			sb.Append("void: " + ShtNumFmt.VOIDsnf.Ordinal + " = " + ShtNumFmt.VOIDsnf);
		//
		//			snf = ShtNumFmt.LEVEL1;
		//			sb.Append(nl).Append("Level1: ").Append(snf.Ordinal).Append(" = ").Append(snf).Append(" = ").Append(snf.Format);
		//			sb.Append(nl).Append("Formatted sheet number: ").Append(snf.FormatSheetFileName("9999", "T", "2", "0", "1", "name"));
		//
		//			snf = ShtNumFmt.LEVEL2;
		//			sb.Append(nl).Append("Level2: ").Append(snf.Ordinal).Append(" = ").Append(snf).Append(" = ").Append(snf.Format);
		//			sb.Append(nl).Append("Formatted sheet number: ").Append(snf.FormatSheetFileName("9999", "T", "2", "0", "1", "name"));
		//
		//			snf = ShtNumFmt.LEVEL3;
		//			sb.Append(nl).Append("Level3: ").Append(snf.Ordinal).Append(" = ").Append(snf).Append(" = ").Append(snf.Format);
		//
		//			sb.Append(nl).Append("Formatted sheet number: ").Append(snf.FormatSheetFileName("9999", "T", "2", "0", "1", "name"));
		//
		//			return sb;
		//		}
		//
		//
		//		private StringBuilder ListProgm()
		//		{
		//			StringBuilder sb = new StringBuilder();
		//
		//			sb.Append("void: " + Software.VOIDsw.Ordinal + " = " + Software.VOIDsw);
		//			sb.Append(nl + "acad: " + Software.AutoCAD.Ordinal + " = " + Software.AutoCAD);
		//			sb.Append(nl + "revt: " + Software.Revit.Ordinal + " = " + Software.Revit);
		//
		//			sb.Append(nl);
		//
		//			sb.Append(nl + "access using index:");
		//
		//			Software p = new Software();
		//
		//			Software z = Software.AutoCAD;
		//			Software y = Software.AutoCAD;
		//
		//			for (int i = 0; i < 3; i++)
		//			{
		//				sb.Append(nl + "index " + i + ": " + p[i]);
		//			}
		//
		//			sb.Append(nl);
		//			sb.Append(nl + "access using foreach & name:");
		//
		//			foreach (Software x in p)
		//			{
		//				sb.Append(nl + "index " + x.Ordinal + ": " + x.Name);
		//			}
		//
		//			sb.Append(nl + nl + "finding Revit (2): " + Software.Find("REVIT").Ordinal);
		//
		//			sb.Append(nl + nl + "finding test (not found): " + Software.Find("TEST").Ordinal);
		//
		//			sb.Append(nl + nl + "to string: " + Software.AutoCAD);
		//
		//			sb.Append(nl + nl + "is member (false): " + Software.IsMember("test"));
		//
		//			sb.Append(nl + nl + "is member (true): " + Software.IsMember("VOIDsnf"));
		//
		//			sb.Append(nl + nl + "count: " + p.Count);
		//
		//			sb.Append(nl + nl + "size: " + Software.Size);
		//
		//			sb.Append(nl + nl + "equal 1: " + z.Equals(y));
		//			sb.Append(nl + "equal 2: " + (z == y));
		//
		//
		//			sb.Append(nl + nl + "hash code: " + z.GetHashCode() + "  " + y.GetHashCode());
		//
		//			sb.Append(nl + nl + "switch?");
		//
		//
		//
		//			sb.Append(nl + nl + "array:");
		//
		//			Software[] xp = p.ToArray();
		//
		//
		//
		//			for (int i = 0; i < xp.Count(); i++)
		//			{
		//				sb.Append(nl + "index " + i + " ordinal: " + xp[i].Ordinal + ": " + xp[i].Name);
		//			}
		//
		//			return sb;
		//		}

	}



}