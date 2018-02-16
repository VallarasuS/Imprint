using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Imprint.IO.Runtime
{
	public class XMLWriter
	{
		private string _path;
		private DataTable _dataTable;

		private XMLWriter(string file, DataTable dataTable)
		{
			_path = file;
			_dataTable = dataTable;
		}

		public static XMLWriter Create(string file, DataTable dataset)
		{
			return new XMLWriter(file, dataset);
		}

		public string Write()
		{
			if (_dataTable == null) throw new ArgumentException("Argument 'dataTable' is null");

			if (_dataTable.Columns == null || _dataTable.Columns.Count == 0 || _dataTable.Rows == null || _dataTable.Rows.Count == 0) throw new ArgumentException("DataTable contains no data");

			if (string.IsNullOrEmpty(_path)) throw new ArgumentException("Argument 'file' is null");

			var header = _dataTable.Columns.OfType<DataColumn>().Select(c => c.Caption).ToArray();

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			using (StreamWriter streamWriter = new StreamWriter(_path))
			{
				using (XmlWriter writer = XmlWriter.Create(streamWriter, settings))
				{
					writer.WriteStartElement("Root");

					foreach (var row in _dataTable.Rows.OfType<DataRow>())
					{
						int index = 0;

						writer.WriteStartElement("Item");

						foreach (var item in row.ItemArray)
						{
							writer.WriteAttributeString(header[index], item.ToString());

							index++;
						}

						writer.WriteEndElement();
					}

					writer.WriteEndElement();
					writer.Flush();
				}

				streamWriter.Flush();
			}

			return string.Empty;
		}
	}
}