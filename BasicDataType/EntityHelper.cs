using System;
using System.Collections.Generic;
using System.Reflection;

namespace MTLibrary
{
    public class EntityHelper
    {
        #region 

        /// <summary>
        /// 将object实体类对象强转Map键值对应类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToMap(object o)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            return ToMap(o, map);
        }

        /// <summary>
        /// 将object实体类对象强转Map键值对应类型并合并传入Map
        /// </summary>
        /// <param name="o"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToMap(object o, Dictionary<string, object> map)
        {
            try
            {
                Type t = o.GetType();
                PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo p in pi)
                {
                    MethodInfo mi = p.GetGetMethod();

                    if (mi != null && mi.IsPublic)
                    {
                        map.Add(p.Name, mi.Invoke(o, new object[] { }));
                    }
                }
                return map;
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// 合并两个Map
        /// </summary>
        /// <param name="dicFirst"></param>
        /// <param name="dicSecond"></param>
        /// <returns></returns>
        public static Dictionary<string, string> MergeMap(Dictionary<string, string> dicFirst, Dictionary<string, string> dicSecond)
        {
            try
            {
                if (dicFirst == null || dicSecond == null) return new Dictionary<string, string>();
                foreach (var item in dicSecond)
                {
                    if (!dicFirst.ContainsKey(item.Key))
                        dicFirst.Add(item.Key, item.Value);
                }

                return dicFirst;
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
  
        }


        /// <summary>
        /// 实体类相同字段内容进行复制
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="s">数据源实体</param>
        /// <returns>返回的新实体</returns>
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>(); //构造新实例
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return d;
        }

    
        /// <summary>
        /// Map强转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T MapToEntity<T>(Dictionary<string, object> dic)
        {
            T model = Activator.CreateInstance<T>();  //泛型的初始化
            try
            {
                PropertyInfo[] modelPro = model.GetType().GetProperties(); //获取类型T中所有的属性字段
                if (modelPro.Length > 0 && dic.Count > 0)
                {
                    for (int i = 0; i < modelPro.Length; i++)
                    {
                        if (dic.ContainsKey(modelPro[i].Name)) //判断dic中Key是否和实体类中的属性名相同,相同则保存。也就是想要什么数据定义什么有属性的实体类
                        {
                            modelPro[i].SetValue(model, dic[modelPro[i].Name], null);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        
            return model;
        }

        #endregion
    }
}
