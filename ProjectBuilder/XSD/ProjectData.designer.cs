// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.41537 Microsoft Reciprocal License (Ms-RL) 
//    <NameSpace>ProjectBuilder</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>True</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>True</IncludeSerializeMethod><UseBaseClass>True</UseBaseClass><GenBaseClass>True</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><OrderXMLAttrib>True</OrderXMLAttrib><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>True</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>False</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace ProjectBuilder
{
	using System;
	using System.Diagnostics;
	using System.Xml.Serialization;
	using System.Collections;
	using System.Xml.Schema;
	using System.ComponentModel;
	using System.IO;
	using System.Text;
	using System.Collections.Generic;


	#region Base entity class
	public partial class ProjectDataBase<T>
	{

		private static System.Xml.Serialization.XmlSerializer serializer;

		private static System.Xml.Serialization.XmlSerializer Serializer
		{
			get
			{
				if ((serializer == null))
				{
					serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
				}
				return serializer;
			}
		}

		#region Serialize/Deserialize
		/// <summary>
		/// Serializes current ProjectDataBase object into an XML document
		/// </summary>
		/// <returns>string XML value</returns>
		public virtual string Serialize()
		{
			System.IO.StreamReader streamReader = null;
			System.IO.MemoryStream memoryStream = null;
			try
			{
				memoryStream = new System.IO.MemoryStream();
				Serializer.Serialize(memoryStream, this);
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
				streamReader = new System.IO.StreamReader(memoryStream);
				return streamReader.ReadToEnd();
			}
			finally
			{
				if ((streamReader != null))
				{
					streamReader.Dispose();
				}
				if ((memoryStream != null))
				{
					memoryStream.Dispose();
				}
			}
		}

		/// <summary>
		/// Deserializes workflow markup into an ProjectDataBase object
		/// </summary>
		/// <param name="xml">string workflow markup to deserialize</param>
		/// <param name="obj">Output ProjectDataBase object</param>
		/// <param name="exception">output Exception value if deserialize failed</param>
		/// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		public static bool Deserialize(string xml, out T obj, out System.Exception exception)
		{
			exception = null;
			obj = default(T);
			try
			{
				obj = Deserialize(xml);
				return true;
			}
			catch (System.Exception ex)
			{
				exception = ex;
				return false;
			}
		}

		public static bool Deserialize(string xml, out T obj)
		{
			System.Exception exception = null;
			return Deserialize(xml, out obj, out exception);
		}

		public static T Deserialize(string xml)
		{
			System.IO.StringReader stringReader = null;
			try
			{
				stringReader = new System.IO.StringReader(xml);
				return ((T)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
			}
			finally
			{
				if ((stringReader != null))
				{
					stringReader.Dispose();
				}
			}
		}

		/// <summary>
		/// Serializes current ProjectDataBase object into file
		/// </summary>
		/// <param name="fileName">full path of outupt xml file</param>
		/// <param name="exception">output Exception value if failed</param>
		/// <returns>true if can serialize and save into file; otherwise, false</returns>
		public virtual bool SaveToFile(string fileName, out System.Exception exception)
		{
			exception = null;
			try
			{
				SaveToFile(fileName);
				return true;
			}
			catch (System.Exception e)
			{
				exception = e;
				return false;
			}
		}

		public virtual void SaveToFile(string fileName)
		{
			System.IO.StreamWriter streamWriter = null;
			try
			{
				string xmlString = Serialize();
				System.IO.FileInfo xmlFile = new System.IO.FileInfo(fileName);
				streamWriter = xmlFile.CreateText();
				streamWriter.WriteLine(xmlString);
				streamWriter.Close();
			}
			finally
			{
				if ((streamWriter != null))
				{
					streamWriter.Dispose();
				}
			}
		}

		/// <summary>
		/// Deserializes xml markup from file into an ProjectDataBase object
		/// </summary>
		/// <param name="fileName">string xml file to load and deserialize</param>
		/// <param name="obj">Output ProjectDataBase object</param>
		/// <param name="exception">output Exception value if deserialize failed</param>
		/// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		public static bool LoadFromFile(string fileName, out T obj, out System.Exception exception)
		{
			exception = null;
			obj = default(T);
			try
			{
				obj = LoadFromFile(fileName);
				return true;
			}
			catch (System.Exception ex)
			{
				exception = ex;
				return false;
			}
		}

		public static bool LoadFromFile(string fileName, out T obj)
		{
			System.Exception exception = null;
			return LoadFromFile(fileName, out obj, out exception);
		}

		public static T LoadFromFile(string fileName)
		{
			System.IO.FileStream file = null;
			System.IO.StreamReader sr = null;
			try
			{
				file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
				sr = new System.IO.StreamReader(file);
				string xmlString = sr.ReadToEnd();
				sr.Close();
				file.Close();
				return Deserialize(xmlString);
			}
			finally
			{
				if ((file != null))
				{
					file.Dispose();
				}
				if ((sr != null))
				{
					sr.Dispose();
				}
			}
		}
		#endregion
	}
	#endregion

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd", IsNullable = false)]
	public partial class ProjectData
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private ProjectDataProject projectField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private List<ProjectDataTask> tasksField;

		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public ProjectDataProject Project
		{
			get
			{
				return this.projectField;
			}
			set
			{
				this.projectField = value;
			}
		}

		[System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
		[System.Xml.Serialization.XmlArrayItemAttribute("Task", IsNullable = false)]
		public List<ProjectDataTask> Tasks
		{
			get
			{
				return this.tasksField;
			}
			set
			{
				this.tasksField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	public partial class ProjectDataProject : ProjectDataBase<ProjectDataProject>
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string rootFolderField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string idField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string descriptionField;

		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public string RootFolder
		{
			get
			{
				return this.rootFolderField;
			}
			set
			{
				this.rootFolderField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	public partial class ProjectDataTask
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private List<ProjectDataTaskPhase> phaseField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string idField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string descriptionField;

		[System.Xml.Serialization.XmlElementAttribute("Phase", Order = 0)]
		public List<ProjectDataTaskPhase> Phase
		{
			get
			{
				return this.phaseField;
			}
			set
			{
				this.phaseField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public override string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public override string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	public partial class ProjectDataTaskPhase
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private List<ProjectDataTaskPhaseBldg> bldgField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string idField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string descriptionField;

		[System.Xml.Serialization.XmlElementAttribute("Bldg", Order = 0)]
		public List<ProjectDataTaskPhaseBldg> Bldg
		{
			get
			{
				return this.bldgField;
			}
			set
			{
				this.bldgField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public override string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public override string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	public partial class ProjectDataTaskPhaseBldg
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string cDFolderField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string sheetNumberFormatField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private ProjectDataTaskPhaseBldgLocation locationField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string idField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string descriptionField;

		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public string CDFolder
		{
			get
			{
				return this.cDFolderField;
			}
			set
			{
				this.cDFolderField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 1)]
		public string SheetNumberFormat
		{
			get
			{
				return this.sheetNumberFormatField;
			}
			set
			{
				this.sheetNumberFormatField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 2)]
		public ProjectDataTaskPhaseBldgLocation Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public override string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		[System.Xml.Serialization.XmlAttributeAttribute()]
		public override string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}
	}

	//		[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	//		[System.SerializableAttribute()]
	//		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	//		public enum ProjectDataTaskPhaseBldgSheetNumberFormat
	//		{
	//	
	//			/// <remarks/>
	//			[System.Xml.Serialization.XmlEnumAttribute("1:X#")]
	//			Item1X,
	//	
	//			/// <remarks/>
	//			[System.Xml.Serialization.XmlEnumAttribute("2:X#.#")]
	//			Item2X,
	//	
	//			/// <remarks/>
	//			[System.Xml.Serialization.XmlEnumAttribute("3:X#.#-#")]
	//			Item3X,
	//		}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	public partial class ProjectDataTaskPhaseBldgLocation
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private ProjectDataTaskPhaseBldgLocationAutoCAD autoCADField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private ProjectDataTaskPhaseBldgLocationRevit revitField;

		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public ProjectDataTaskPhaseBldgLocationAutoCAD AutoCAD
		{
			get
			{
				return this.autoCADField;
			}
			set
			{
				this.autoCADField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 1)]
		public ProjectDataTaskPhaseBldgLocationRevit Revit
		{
			get
			{
				return this.revitField;
			}
			set
			{
				this.revitField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	public partial class ProjectDataTaskPhaseBldgLocationAutoCAD
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string sheetFolderField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string xrefFolderField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string detailFolderField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string borderFileField;

		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public string SheetFolder
		{
			get
			{
				return this.sheetFolderField;
			}
			set
			{
				this.sheetFolderField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 1)]
		public string XrefFolder
		{
			get
			{
				return this.xrefFolderField;
			}
			set
			{
				this.xrefFolderField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 2)]
		public string DetailFolder
		{
			get
			{
				return this.detailFolderField;
			}
			set
			{
				this.detailFolderField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 3)]
		public string BorderFile
		{
			get
			{
				return this.borderFileField;
			}
			set
			{
				this.borderFileField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "file:///D:/Users/Jeff/Documents/Programming/VisualStudioProjects/ProjectBuilder/ProjectBuilder/XSD/ProjectData.xsd")]
	public partial class ProjectDataTaskPhaseBldgLocationRevit
	{

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string cDModelFileField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string libraryModelFileField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string keynoteFileField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string linkedFolderField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		private string xrefFolderField;

		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public string CDModelFile
		{
			get
			{
				return this.cDModelFileField;
			}
			set
			{
				this.cDModelFileField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 1)]
		public string LibraryModelFile
		{
			get
			{
				return this.libraryModelFileField;
			}
			set
			{
				this.libraryModelFileField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 2)]
		public string KeynoteFile
		{
			get
			{
				return this.keynoteFileField;
			}
			set
			{
				this.keynoteFileField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 3)]
		public string LinkedFolder
		{
			get
			{
				return this.linkedFolderField;
			}
			set
			{
				this.linkedFolderField = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(Order = 4)]
		public string XrefFolder
		{
			get
			{
				return this.xrefFolderField;
			}
			set
			{
				this.xrefFolderField = value;
			}
		}
	}
}
