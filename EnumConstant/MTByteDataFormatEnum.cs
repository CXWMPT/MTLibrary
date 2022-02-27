using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    /// <summary>
    ///  应用于多字节数据的解析或是生成格式
    /// </summary>
    public enum MTByteDataFormatEnum
    {
        //
        // 摘要:
        //     按照顺序排序
        AB = 20,
        //
        // 摘要:
        //     按照单字反转
        BA = 21,
        //
        // 摘要:
        //     按照顺序排序
        ABCD = 40,
        //
        // 摘要:
        //     按照单字反转
        BADC = 41,
        //
        // 摘要:
        //     按照双字反转
        CDAB = 42,
        //
        // 摘要:
        //     按照倒序排序
        DCBA = 43
    }
}