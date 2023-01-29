using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Project_DataBase
{
    public class Functions
    {
  
           public static string DataTableToJSON(DataTable table)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"" + table.Columns[j].ColumnName + "\":\"" + table.Rows[i][j].ToString() + "\"");
                    if (j < table.Columns.Count - 1)
                    {
                        jsonBuilder.Append(",");
                    }
                }
                jsonBuilder.Append("}");
                if (i < table.Rows.Count - 1)
                {
                    jsonBuilder.Append(",");
                }
            }
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
    }
}