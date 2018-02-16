using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imprint.IO.Runtime
{
	public class HTMLElement
	{
		public Element ElementType { get; set; }

		public string TagName { get; set; }

		public string ClassName { get; set; }

		public string ID { get; set; }

		public string Xpath { get; set; }

		public string Header { get; set; }
	}
}