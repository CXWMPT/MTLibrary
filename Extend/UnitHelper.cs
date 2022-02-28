using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    public class UnitHelper
    {

        #region 
        /// <summary>
        /// 质量单位转换的比例值
        /// </summary>
        /// <param name="sourceEnum">传入的质量单位枚举</param>
        /// <param name="destEnum">转换的质量单位枚举</param>
        /// <returns></returns>
        public static decimal MassUnitConversion(MTMassUnitEnum sourceEnum, MTMassUnitEnum destEnum)
        {
            if ((int)sourceEnum < MassUnitConversionValue.Length && (int)destEnum < MassUnitConversionValue.Length)
            {
                return MassUnitConversionValue[(int)sourceEnum] / MassUnitConversionValue[(int)destEnum];
            }
            else
            {
                return 1;
            }

        }

        #endregion


        #region 内部变量方法

        private static decimal[] MassUnitConversionValue = new decimal[]
    {
            0.000000000001m,
            0.000000001m,
            0.000001m,
            0.001m,
            1,
            1000,
            100,
            0.0002m,
            0.000002m,


            0.45359237m,
            0.028349523125m,
            0.00006479891m,
            1016.0469088m,
            907.18474m,
            50.80234544m,
            45.359237m,
            6.35029318m,
            0.0011771845195313m,
            50,
            0.5m,
            0.05m,
            0.005m,
            1,
    };
        #endregion

      
    }
}
