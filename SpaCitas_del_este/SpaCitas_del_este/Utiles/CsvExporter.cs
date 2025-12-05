using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaCitas_del_este.Utiles
{
    public class CsvExporter
    {
        public static void ExportDataGridViewToCsv(DataGridView dgv, string filePath)
        {
            var sb = new StringBuilder();
            var headers = string.Join(",", dgv.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText));
            sb.AppendLine(headers);

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>().Select(c => c.Value?.ToString() ?? "");
                    sb.AppendLine(string.Join(",", cells));
                }
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

        }
    }
}
