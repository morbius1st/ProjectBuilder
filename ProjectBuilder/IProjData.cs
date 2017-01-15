using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace ProjectBuilder
{
	public interface IProjDataA
	{
		
		bool Update(ProjData pdNew);
	}

	public interface IProjDataB : IProjDataA
	{
		[XmlIgnore]
		string ID { get; set; }

		[XmlIgnore]
		string Description { get; set; }

		List<FindItem> FindItems(UserProj uProj, int level);
		void Sort();
		IDInfo GetID();
	}
}