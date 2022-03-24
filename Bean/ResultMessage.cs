
namespace NTDCommLib
{
    /// <summary>
    /// http交互返回的类
    /// </summary>
    public class ResultMessage
    {
        public bool status;
        public int? code;
        public string message;
        public object data;
        public int? count;
        public int? total;


        public ResultMessage(bool status, int? code, string message, object data, int? count, int? total)
        {
            this.status = status;
            this.code = code;
            this.message = message;
            this.data = data;
            this.count = count;
            this.total = total;
        }

        private static ResultMessage create(bool status, int? code, string message, object data, int? count, int? total)
        {
            return new ResultMessage(status, code, message, data, count, total);
        }

        public static ResultMessage success()
        {
            return create(true, 0, "success", null, null, null);
        }

        public static ResultMessage success(string message)
        {
            return create(true, 0, message, null, null, null);
        }

        public static ResultMessage success(string message, object data)
        {
            return create(true, 0, message, data, null, null);
        }
        public static ResultMessage success(string message, object data, int? count)
        {
            return create(true, 0, "success", data, count, null);
        }
        public static ResultMessage success(string message, object data, int? count, int? total)
        {
            return create(true, 0, message, data, count, total);
        }





        public static ResultMessage error()
        {
            return create(false, -1, "error", null, null, null);
        }
        public static ResultMessage error(string message)
        {
            return create(false, -1, message, null, null, null);
        }
        public static ResultMessage error(object data)
        {
            return create(false, -1, "error", data, null, null);
        }
        public static ResultMessage error(string message, object data)
        {
            return create(false, -1, message, data, null, null);
        }
    }
}
