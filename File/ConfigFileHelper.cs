using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MTLibrary
{
    /// <summary>
    /// 配置文件操作
    /// </summary>
    public class ConfigFileHelper
    {

        #region 

        /// <summary>
        ///保存配置文件的类到指定路径[文件类不能包含其他类]
        /// </summary>
        /// <param name="pathname">配置文件路径及文件名</param>
        public static void SaveXml<T>(string pathName, ref T settings) where T : class, new()
        {
            //save profile
            //  settings = new T();
            try
            {
                // 判断配置文件是否存在，不存在则创建
                if (!File.Exists(pathName))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(pathName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pathName));
                    }
                    //File.WriteAllText(pathname, null);
                    File.Create(pathName).Dispose();//创建完文件关闭，防止文件被占用;
                }

                // 保存配置数据
                using (FileStream fs = new FileStream(pathName, FileMode.Create, FileAccess.Write))
                {
                    XmlWriterSettings xws = new XmlWriterSettings()
                    {
                        Indent = true,
                        NamespaceHandling = NamespaceHandling.OmitDuplicates,
                        OmitXmlDeclaration = false,
                        Encoding = Encoding.UTF8,
                        NewLineChars = "\r\n"
                    };
                    XmlWriter writer = XmlWriter.Create(fs, xws);
                    XmlSerializer xs = new XmlSerializer(settings.GetType());
                    xs.Serialize(writer, settings);
                    writer.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 读取配置文件的内容返回到配置文件的类的属性上
        /// </summary>
        /// <param name="pathname">配置文件路径及文件名</param>
        /// <returns>配置文件类</returns>
        public static T ReadXml<T>(string pathName) where T : class, new()
        {
            T settings = new T();
            try
            {
                using (FileStream fs = new FileStream(pathName, FileMode.Open, FileAccess.Read))
                {
                    XmlReader reader = XmlReader.Create(fs);
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    settings = xs.Deserialize(reader) as T;
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return settings;
        }
        #endregion

    }
}
