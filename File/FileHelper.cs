using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NTDCommLib
{
    public class FileHelper
    {

        #region 

        /// <summary>
        ///获得目录下所有文件或指定文件类型文件[包含所有子文件夹]
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extName">扩展名可以多个 例如 .mp3.wma.rm</param>
        /// <returns>List<FileInfo></returns>
        public static List<FileInfo> GetFile(string path, string extName)
        {
            return GetFile(path, extName, true);
        }

        /// <summary>
        /// 获得目录下所有文件或指定文件类型文件[包含所有子文件夹]
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extName">扩展名可以多个 例如 .mp3.wma.rm</param>
        /// <param name="isIncludeChildFiles">是否包含包含子文件夹</isIncludeChildFiles>
        /// <returns>List<FileInfo></returns>
        public static List<FileInfo> GetFile(string path, string extName, bool isIncludeChildFiles)
        {
            try
            {
                List<FileInfo> lst = new List<FileInfo>();
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo item in file) //显示当前目录所有文件   
                    {
                        if (string.IsNullOrEmpty(item.Extension.ToLower())) continue;
                        if (extName.ToLower().IndexOf(item.Extension.ToLower()) >= 0) lst.Add(item);
                    }
                    if (isIncludeChildFiles)
                    {
                        foreach (string d in dir)
                        {
                            //递归   
                            foreach (var item in GetFile(d, extName, isIncludeChildFiles))
                            {
                                lst.Add(item);
                            }
                        }
                    }

                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<FileInfo>();
            }
            return new List<FileInfo>();
        }

        /// <summary>
        /// 新文件替换，不存在就新增[如果源文件被占用将替换失败]
        /// </summary>
        /// <param name="sourceFileName">源文件</param>
        /// <param name="destFileName">目标文件</param>
        /// <returns></returns>
        public static bool FileReplace(string sourceFileName, string destFileName)
        {
            try
            {
                string msg = string.Empty;
                return FileReplace(sourceFileName,destFileName, out  msg);
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        /// <summary>
        /// 新文件替换，不存在就新增,带返回值[如果源文件被占用将替换失败]
        /// </summary>
        /// <param name="sourceFileName">源文件</param>
        /// <param name="destFileName">目标文件</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool FileReplace(string sourceFileName, string destFileName, out string msg)
        {
            msg = string.Empty;
            try
            {
                if (!File.Exists(sourceFileName))
                {
                    msg = sourceFileName + "文件不存在";
                    return false;
                }
                if (!File.Exists(destFileName))
                {
                    //文件不存在就创建文件和文件夹
                    if (!Directory.Exists(Path.GetDirectoryName(destFileName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destFileName));
                    }

                    File.Create(destFileName).Dispose(); //创建完文件关闭，防止文件被占用;
                }
                File.Copy(sourceFileName, destFileName, true);
                msg = "操作成功";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 根据路径删除文件或文件夹[如果源文件被占用将删除失败]
        /// </summary>
        /// <param name="path"></param>
        public static bool DeleteFile(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) return false;
                if (File.Exists(path))
                {
                    // 3.2、删除文件
                    File.Delete(path);
                    return true;
                }
                else if (Directory.Exists(path))
                {
                    // 3.1、删除文件夹
                    Directory.Delete(path, true);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 创建文件和文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFile(string path)
        {

            if (!File.Exists(path))
            {
                //文件不存在就创建文件和文件夹
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                File.Create(path).Dispose(); //创建完文件关闭，防止文件被占用;
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string FileToString(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "";
            }
            Encoding encoding = Encoding.UTF8;
            StreamReader reader = null;
            string fileData = string.Empty; ;
            try
            {
            load:
                reader = new StreamReader(filePath, encoding, true);
                fileData = reader.ReadToEnd();
                if (fileData.Contains("�") || fileData.Contains("★") || fileData.Contains("╀") || fileData.Contains("??"))
                {
                    switch (encoding.WebName)
                    {
                        case "utf-8":
                            encoding = Encoding.Default;
                            break;
                        case "GB2312":
                            encoding = Encoding.ASCII;
                            break;
                        case "us-ascii":
                            encoding = Encoding.UTF7;
                            break;
                        case "utf-7":
                            encoding = Encoding.Unicode;
                            break;
                        default:

                            return "";
                    }
                    goto load;
                }

            }
            catch (Exception ex)
            {

                return "";
            }
            finally
            {
                reader.Close();
            }

            return fileData;
        }


        #endregion


        #region 废弃方法


        /// <summary>
        /// 选择文件
        /// </summary>
        /// <returns></returns>
        public static string SelectPath()
        {
            string path = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "";
            ofd.Filter = "All Files (*.*)|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
            }

            return path;
        }
        // 选择路径
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <returns></returns>
        public static string SelectFolder()
        {
            string path = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            return path;
        }
        public static SaveFileDialog WinFormSaveFileDialog(string filter)
        {
            //"Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //设置文件类型
                saveFileDialog.Filter = filter;
                //sfd.FileName = "保存";//设置默认文件名
                // sfd.DefaultExt = "csv";//设置默认格式（可以不设） 
                //设置自动在文件名中添加扩展名
                saveFileDialog.AddExtension = true;
                //设置默认文件类型显示顺序 
                saveFileDialog.FilterIndex = 1;
                //保存对话框是否记忆上次打开的目录 
                saveFileDialog.RestoreDirectory = true;
                //检查目录
                saveFileDialog.CheckPathExists = true;

                return saveFileDialog;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public static OpenFileDialog WinFormOpenFileDialog(string filter)
        {
            //"Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                //设置文件过滤类型
                openFileDialog.Filter = filter;
                //设置文件打开初始目录为E盘
                openFileDialog.InitialDirectory = @"C:\";
                //设置打开文件对话框标题
                openFileDialog.Title = "打开文件";
                openFileDialog.ValidateNames = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.CheckFileExists = false;
                //设置默认文件类型显示顺序 
                openFileDialog.FilterIndex = 2;
                //保存对话框是否记忆上次打开的目录 
                openFileDialog.RestoreDirectory = true;

                return openFileDialog;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public static FolderBrowserDialog WinFormFolderBrowserDialog(string filter)
        {
            try
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                //设置根目录在桌面；
                folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
                //设置当前选择的路径
                folderBrowserDialog.SelectedPath = "C:";
                //允许在对话框中包括一个新建目录的按钮
                folderBrowserDialog.ShowNewFolderButton = true;
                //设置对话框的说明信息
                folderBrowserDialog.Description = "请选择输出目录";

                return folderBrowserDialog;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion
        #region .net3.5不支持

        #endregion

        #region 内部常量方法

        #endregion


    }

}
