using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SimpleAuthResult
    {
        public string Error { get; set; }
        public bool IsSuccess { get; set; }

        public SimpleAuthResult(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static SimpleAuthResult Success()
        {
            return new SimpleAuthResult(true, string.Empty);
        }

        public static SimpleAuthResult Fail(string error)
        {
            return new SimpleAuthResult(false, error);
        }
    }

    public class SimpleAuthResult<T> where T : new()
    {
        public string Error { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

        public SimpleAuthResult(bool isSuccess, string error, T data)
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }

        public static SimpleAuthResult<T> Success(T data)
        {
            return new SimpleAuthResult<T>(true, string.Empty, data);
        }

        public static SimpleAuthResult<T> Fail(string error)
        {
            return new SimpleAuthResult<T>(false, error, new T());
        }
    }
}
