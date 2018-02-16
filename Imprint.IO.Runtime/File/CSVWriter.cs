using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imprint.IO.Runtime
{
	public class CSVWriter
	{
		private string _path;
		private DataTable _dataTable;

		private CSVWriter(string file, DataTable dataTable)
		{
			_path = file;
			_dataTable = dataTable;
		}

		public static CSVWriter Create(string file, DataTable dataset)
		{
			return new CSVWriter(file, dataset);
		}

		public string Write()
		{
			try
			{
				if (_dataTable == null) throw new ArgumentException("Argument 'dataTable' is null");

				if (_dataTable.Columns == null || _dataTable.Columns.Count == 0 || _dataTable.Rows == null || _dataTable.Rows.Count == 0) throw new ArgumentException("DataTable contains no data");

				if (string.IsNullOrEmpty(_path)) throw new ArgumentException("Argument 'file' is null");

                var exists = File.Exists(_path);

				using (StreamWriter writer = new StreamWriter(_path, exists))
				{
                    if (!exists)
                    {
                        var header = string.Join(",", _dataTable.Columns.OfType<DataColumn>().Select(c => c.Caption));

                        writer.WriteLine(header);
                    }

					foreach (DataRow row in _dataTable.Rows)
					{
						var rowData = string.Join(",", row.ItemArray.Select(i => string.Format("\"{0}\"", i.ToString())));

						writer.WriteLine(rowData);
					}

					writer.Flush();
				}
			}
			catch (Exception e)
			{
				return e.Message;
			}

			return string.Empty;
		}
	}
}