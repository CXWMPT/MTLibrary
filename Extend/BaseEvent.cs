using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MTLibrary
{
    //执行触发事件时调用
    //继承微软基类事件
    public class BaseEvent<T> : EventArgs
    {
        //错误码
        public int Code { get; private set; }
        //提示信息
        public string Message { get; private set; } = string.Empty;
        //返回具体内容
        public T Data { get; private set; }
        //表格总条数
        public int Count { get; private set; }
        public int Total { get; private set; }

        //public MeterEntity WeightData { get; private set; }
        public BaseEvent()
        {
            Code = 0;
            Message = string.Empty;
        }
        public BaseEvent(T data)
        {
            Code = 0;
            Message = string.Empty;
            Data = data;
        }

        public BaseEvent(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public BaseEvent(int code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
