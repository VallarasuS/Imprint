using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imprint.IO.Runtime
{
	public enum Events
	{
		MouseUp
	}

	public enum Selector
	{
		Element,

		ID,

		Class
	}

	public enum Element
	{
		None,

 		Text,

		Image,

		Url,

		List,

		Table,

        Next
	}
}