﻿using NTDCommLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NTDCommLib
{

    public class NTDWeightCoreBean
    {

        public void SetUnit(string unit)
        {
            try
            {
                Unit = EnumHelper.Conversion<NTDMassUnitEnum>(unit);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 设置累计称重值
        /// </summary>
        /// <param name="weight"></param>
        public void SetCumulativeNetWeight(string weight)
        {
            CumulativeNetWeight = (DecimalHelper.Conversion(weight) + DecimalHelper.Conversion(CumulativeNetWeight)).ToString();
        }

        /// <summary>
        /// 获取净重
        /// </summary>
        /// <returns></returns>
        public string GetNetWeight()
        {
            return (DecimalHelper.ObjectConversionDecimal(NetWeight) - DecimalHelper.ObjectConversionDecimal(PresetTareWeight)).ToString();
        }

        /// <summary>
        /// 获取毛重
        /// </summary>
        /// <returns></returns>
        public string GetGrossWeight()
        {
            return (DecimalHelper.ObjectConversionDecimal(NetWeight) + DecimalHelper.ObjectConversionDecimal(TareWeight)).ToString();
        }

        /// <summary>
        /// 获取称台皮重和软件预置皮重的重量
        /// </summary>
        /// <returns></returns>
        public string GetTareWeight()
        {
            return (DecimalHelper.ObjectConversionDecimal(TareWeight) + DecimalHelper.ObjectConversionDecimal(PresetTareWeight) + DecimalHelper.ObjectConversionDecimal(NetWeight) - DecimalHelper.ObjectConversionDecimal(NetWeight)).ToString();
        }

        /// <summary>
        /// 内部秤台的称号（1,2）
        /// </summary>
        public int PlatformNo = 1;

        /// <summary>
        /// 赋值显示重量
        /// </summary>
        public string Weight { get; set; } = "0";
        /// <summary>
        /// 设置皮重
        /// </summary>
        public string TareWeight { get; set; } = "0";
        /// <summary>
        /// 设置净重
        /// </summary>
        public string NetWeight { get; set; } = "0";
        /// <summary>
        /// 设置毛重
        /// </summary>
        public string GrossWeight { get; set; } = "0";
        /// <summary>
        /// 预置皮重(软件去皮)
        /// </summary>
        public string PresetTareWeight { get; set; } = "0";
        /// <summary>
        /// 累计的净重
        /// </summary>
        public string CumulativeNetWeight { get; set; } = "0";

        /// <summary>
        /// 单位
        /// </summary>
        public NTDMassUnitEnum Unit { get; set; } = NTDMassUnitEnum.kg;
        /// <summary>
        /// 是否稳态 true false
        /// </summary>
        public bool Stable { get; set; }

        /// <summary>
        /// 稳态显示的值 - ~
        /// </summary>
        public string StableShowTxt { get; set; } = "~";
        /// <summary>
        /// 满量程
        /// </summary>
        public string Range { get; set; } = "60";
        /// <summary>
        /// 分度值
        /// </summary>
        public string Division { get; set; } = "0";
        /// <summary>
        /// 零点跟踪 几个d
        /// </summary>
        public string ZeroCapture { get; set; } = "0";
        /// <summary>
        /// 稳定范围 几个d
        /// </summary>
        public string StableRange { get; set; } = "0";

        /// <summary>
        /// 零点标志位
        /// </summary>
        public bool ZeroFlag { get; set; } = false;

        /// <summary>
        /// 零点标志的字符串表达式 >0<
        /// </summary>
        public string ZeroFlagStr { get; set; } = ">0<";

     

        /// <summary>
        /// 欠载标志
        /// </summary>
        public bool UnderLoadFlag { get; set; } = false;
        /// <summary>
        /// 欠载标志的字符串表达  欠载 
        /// </summary>
        public string UnderLoadFlagStr { get; set; } = "[______]";
        /// <summary>
        /// 超载标志
        /// </summary>
        public bool OverLoadFlag { get; set; } = false;
        /// <summary>
        /// 超载标志 字符串表达式 超载
        /// </summary>
        public string OverLoadFlagStr { get; set; } = "[¯¯¯¯¯¯]";

        /// <summary>
        /// 超出量程(超载或欠载)
        /// </summary>
        public bool OverstepRange { get; set; } = false;

        /// <summary>
        /// 是否超载或欠载
        /// </summary>
        public bool UnderOrOverLoadFlag
        {
            get
            {
                if (UnderLoadFlag || OverLoadFlag)
                {
                    return true;
                }
                else if (OverstepRange)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 超载或欠载字符串表达 
        /// </summary>
        public string UnderOrOverLoadFlagStr
        {
            get
            {
                if (UnderLoadFlag)
                {
                    return UnderLoadFlagStr;
                }
                else if (OverLoadFlag)
                {
                    return OverLoadFlagStr;
                }
                else if (OverstepRange)
                {
                    if (DecimalHelper.Conversion(GetGrossWeight()) > 0)
                    {
                        return OverLoadFlagStr;
                    }
                    else
                    {
                        return UnderLoadFlagStr;
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 日期时间
        /// </summary>
        public string DateTime = "";
        /// <summary>
        /// 日期
        /// </summary>
        public string Date = "";
        /// <summary>
        /// 时间
        /// </summary>
        public string Time = "0";
        /// <summary>
        /// 按键的值
        /// </summary>
        public string KeyValue = "";

        /// <summary>
        /// 是否净重
        /// </summary>

        public bool IsNetFlag = false;

        /// <summary>
        /// 是否去过皮
        /// </summary>
        private bool IsSetTareFlagDefaultValue = false;
        public bool IsSetTareFlag { get { return DecimalHelper.Conversion(GetTareWeight()) > 0; } }

    }
}

