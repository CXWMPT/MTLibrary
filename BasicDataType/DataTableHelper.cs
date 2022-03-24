using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MTLibrary
{
    public static class DataTableHelper
    {
        #region

        /// <summary>
        /// 将List集合转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable Conversion<T>(IList<T> list)
        {
            return Conversion(list, new string[0]);
        }


        /// <summary>
        /// 将List集合转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertyName">保留string[]包含的列名，其余列名不进行转换</param>
        /// <returns></returns>
        public static DataTable Conversion<T>(IList<T> list, params string[] propertyName)
        {

            List<string> propertyNameList = new List<string>();
            if (propertyName.Length != 0)
            {
                propertyNameList.AddRange(propertyName);
            }

            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        //if (DBNull.Value.Equals(pi.PropertyType))
                        //{
                        //   // pi.PropertyType = DateTime;
                        //}
                        Type colType = pi.PropertyType;
                        if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        result.Columns.Add(pi.Name, colType);
                        //result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                        {
                            Type colType = pi.PropertyType;
                            if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                colType = colType.GetGenericArguments()[0];
                            }
                            result.Columns.Add(pi.Name, colType);
                        }
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// 将List集合转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="dic">根据Map顺序进行过滤实体类的属性</param>
        /// <returns></returns>
        public static DataTable Conversion<T>(IList<T> list, Dictionary<string, string> dic)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (var item in dic)
                {
                    if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Value)) continue;

                    result.Columns.Add(item.Value);
                }

                ArrayList tempList = new ArrayList();
                for (int i = 0; i < list.Count; i++)
                {
                    tempList = new ArrayList();
                    foreach (var item in dic)
                    {
                        if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Value)) continue;

                        foreach (PropertyInfo pi in propertys)
                        {
                            if (pi.Name == item.Key)
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

    



        /// <summary>
        /// 将Csv文件内容转DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <returns></returns>
        public static DataTable CSVFilePathConversion(string filePath) {

            return CSVFilePathConversion(filePath,1);
        }

        /// <summary>
        /// 将Csv读入DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        public static DataTable CSVFilePathConversion(string filePath, int n)
        {
            //防止文件正在打开不能读取的问题
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default, false);
            DataTable dt = new DataTable();
            try
            {
                int i = 0, m = 0;

                reader.Peek();
                while (reader.Peek() > 0)
                {
                    m = m + 1;
                    string str = reader.ReadLine();
                    string[] split = str.Split(',');
                    if (m < n + 1) //列标题
                    {
                        for (i = 0; i < split.Length; i++)
                        {
                            dt.Columns.Add(split[i]);
                        }
                    }
                    else //具体里面的数据
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        for (i = 0; i < split.Length; i++)
                        {
                            //这个数据必须不"\",只是""
                            dr[i] = split[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            finally
            {
                stream.Close();
                reader.Close();
            }
            return dt;
        }
        #endregion


        #region .net3.5不支持
        /// <summary>
        /// 将DataGridView转成DataTable(wince不支持)
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="isFilter"></param>
        /// <returns></returns>
        public static DataTable Conversion(DataGridView dgv, bool isFilter)
        {
            DataTable tmpDataTable = new DataTable("tmpDataTable");
            DataTable modelTable = new DataTable("ModelTable");
            if (isFilter)
            {
                for (int column = 0; column < dgv.Columns.Count; column++)
                {
                    if (dgv.Columns[column].Visible == true)
                    {
                        DataColumn tempColumn = new DataColumn(dgv.Columns[column].HeaderText, typeof(string));
                        tmpDataTable.Columns.Add(tempColumn);
                        DataColumn modelColumn = new DataColumn(dgv.Columns[column].Name, typeof(string));
                        modelTable.Columns.Add(modelColumn);
                    }
                }
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    if (dgv.Rows[row].Visible == false)
                        continue;
                    DataRow tempRow = tmpDataTable.NewRow();
                    for (int i = 0; i < tmpDataTable.Columns.Count; i++)
                        tempRow[i] = dgv.Rows[row].Cells[modelTable.Columns[i].ColumnName].Value;
                    tmpDataTable.Rows.Add(tempRow);
                }
            }
            else
            {
                for (int column = 0; column < dgv.Columns.Count; column++)
                {
                    DataColumn tempColumn = new DataColumn(dgv.Columns[column].HeaderText, typeof(string));
                    tmpDataTable.Columns.Add(tempColumn);
                    DataColumn modelColumn = new DataColumn(dgv.Columns[column].Name, typeof(string));
                    modelTable.Columns.Add(modelColumn);
                }
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    DataRow tempRow = tmpDataTable.NewRow();
                    for (int i = 0; i < tmpDataTable.Columns.Count; i++)
                        tempRow[i] = dgv.Rows[row].Cells[modelTable.Columns[i].ColumnName].Value;
                    tmpDataTable.Rows.Add(tempRow);
                }
            }
            return tmpDataTable;

        }
        #endregion
    }
}
