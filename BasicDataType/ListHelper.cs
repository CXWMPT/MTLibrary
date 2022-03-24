using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MTLibrary
{
    public class ListHelper
    {

        #region 

        public static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            return DataTableToList<T>(dt,new Dictionary<string, object>());
        }



        /// <summary>
        /// 将datatable转List<T>根据建值对应（数据导入功能使用）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="dicWhere">key= "work_no", value="工号"
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt, Dictionary<string, object> dicWhere) where T : new()
        {

            //定义集合
            List<T> listCollection = new List<T>(dt.Rows.Count);
            //判断DataTable是否为空
            if (dt == null || dt.Rows.Count <= 0) return listCollection;
            ////获得 T 模型类型并获得 T 模型类型公共属性
            PropertyInfo[] propertyInfoArray = typeof(T).GetProperties();
            //临时变量，存储变量模型公共属性Name
            string tempName = string.Empty;
            //遍历参数 DataTable的每行
            foreach (DataRow dataRow in dt.Rows)
            {
                //实例化 T 模版类
                T t = new T();
                //遍历T 模版类各个属性
                #region
                foreach (PropertyInfo propertyInfo in propertyInfoArray)
                {
                    //取出类属性之一
                    tempName = propertyInfo.Name;
                    if (dicWhere!=null&& dicWhere.Count>0) 
                    {
                        tempName = dicWhere.Keys.Contains(tempName) == true ? dicWhere[tempName].ToString() : "";
                    }
                   
                    //判断DataTable中是否有此列
                    if (dt.Columns.Contains(tempName))
                    {
                        //判断属性是否可写属性
                        if (!propertyInfo.CanWrite)
                        {
                            continue;
                        }
                        try
                        {
                            //得到Datable单元格中的值
                            object value = dataRow[tempName];
                            //得到 T 属性类型
                            Type type = propertyInfo.PropertyType;
                            //判断类型赋值
                            if (value != DBNull.Value)
                            {

                                if (value.GetType() == type)
                                {
                                    propertyInfo.SetValue(t, value, null);
                                }
                                else
                                {
                                    if (type == typeof(byte))
                                    {
                                        try
                                        {
                                            byte Temp = Convert.ToByte(value);
                                            propertyInfo.SetValue(t, false, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(byte?))
                                    {
                                        try
                                        {
                                            byte Temp = Convert.ToByte(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(short))
                                    {
                                        try
                                        {
                                            short Temp = short.Parse(value.ToString());
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, 0, null);
                                        }
                                    }
                                    else if (type == typeof(short?))
                                    {
                                        try
                                        {
                                            short Temp = short.Parse(value.ToString());
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(int))
                                    {
                                        try
                                        {
                                            int Temp = int.Parse(value.ToString());
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, 0, null);
                                        }
                                    }
                                    else if (type == typeof(int?))
                                    {
                                        try
                                        {
                                            int Temp = int.Parse(value.ToString());
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(long))
                                    {
                                        try
                                        {
                                            long Temp = long.Parse(value.ToString());
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, 0, null);
                                        }
                                    }
                                    else if (type == typeof(long?))
                                    {
                                        try
                                        {
                                            long Temp = long.Parse(value.ToString());
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(decimal))
                                    {
                                        try
                                        {
                                            decimal Temp = Convert.ToDecimal(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, 0, null);
                                        }
                                    }
                                    else if (type == typeof(decimal?))
                                    {
                                        try
                                        {
                                            decimal Temp = Convert.ToDecimal(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(double))
                                    {
                                        try
                                        {
                                            double Temp = Convert.ToDouble(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, 0, null);
                                        }
                                    }
                                    else if (type == typeof(double?))
                                    {
                                        try
                                        {
                                            double Temp = Convert.ToDouble(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(float))
                                    {
                                        try
                                        {
                                            float Temp = Convert.ToSingle(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, 0, null);
                                        }
                                    }
                                    else if (type == typeof(float?))
                                    {
                                        try
                                        {
                                            float Temp = Convert.ToSingle(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }

                                    else if (type == typeof(DateTime))
                                    {
                                        try
                                        {
                                            DateTime Temp = Convert.ToDateTime(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, DateTime.Now, null);
                                        }
                                    }
                                    else if (type == typeof(DateTime?))
                                    {
                                        try
                                        {
                                            DateTime Temp = Convert.ToDateTime(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(bool))
                                    {
                                        try
                                        {
                                            bool Temp = Convert.ToBoolean(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, DateTime.Now, null);
                                        }
                                    }
                                    else if (type == typeof(bool?))
                                    {
                                        try
                                        {
                                            bool Temp = Convert.ToBoolean(value);
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else if (type == typeof(string))
                                    {
                                        try
                                        {
                                            string Temp = value.ToString();
                                            propertyInfo.SetValue(t, Temp, null);
                                        }
                                        catch
                                        {
                                            propertyInfo.SetValue(t, null, null);
                                        }
                                    }
                                    else
                                    {
                                        object Temp = Convert.ChangeType(value, type, new CultureInfo("zh_CN"));
                                        propertyInfo.SetValue(t, Temp, null);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                #endregion
                listCollection.Add(t);
            }
            return listCollection;
        }

        #endregion

    }
}
