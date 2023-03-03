using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Project_DataBase
{
    public class Functions
    {

        public static T MapDataTableToClass<T>(DataTable dataTable) where T : class, new()
        {
            List<T> objects = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in dataTable.Columns)
                {
                    PropertyInfo property = obj.GetType().GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value)
                    {
                        property.SetValue(obj, row[column]);
                    }
                }
                objects.Add(obj);
            }
            return objects.FirstOrDefault();
        }


        public static List<T> MapDataTableToListOfClass<T>(DataTable dataTable) where T : class, new()
        {
            List<T> objects = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in dataTable.Columns)
                {
                    PropertyInfo property = obj.GetType().GetProperty(column.ColumnName);
                    System.Diagnostics.Debug.WriteLine($"property: {property} Type:{obj.GetType()}" );
                    if (property != null && row[column] != DBNull.Value)
                    {
                        property.SetValue(obj, row[column]);
                    }
                }
                objects.Add(obj);
            }
            return objects;
        }

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