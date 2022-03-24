using System.Collections.Generic;

namespace MTLibrary
{
    public class PageHelper
    {
        #region 
        /// <summary>
        /// 将List实体类进行分页
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="allList">总数据</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="p">一页多少条数据</param>
        /// <returns>分页总页数</returns>
        public static List<T> GetPageList<T>(List<T> allList, int currentPage, int pageCount, out int countPage)
        {
            countPage = 0;//总页数
            if (allList.Count == 0)
            {
                return new List<T>();
            }
            // 计算总页数
            if (allList.Count % pageCount > 0)
            {
                countPage = allList.Count / pageCount + 1;
            }
            else
            {
                countPage = allList.Count / pageCount;
            }

            List<T> listData = new List<T>();

            int pageFirstRec = (currentPage - 1) * pageCount;//开始下标
            int pageLastRec = currentPage * pageCount;//结束下标
            //判断结束的下标是否大于数据总长度
            if (pageLastRec > allList.Count)
            {
                pageLastRec = allList.Count;
            }

            for (int i = pageFirstRec; i < pageLastRec; i++)
            {
                listData.Add(allList[i]);
            }
            return listData;
        }

        #endregion

     

    }
}
