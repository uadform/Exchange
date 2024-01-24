using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public static ServiceResponse<T> Success(T data, int statusCode = 200)
        {
            return new ServiceResponse<T> { Data = data, IsSuccess = true, StatusCode = statusCode };
        }

        public static ServiceResponse<T> Failure(string errorMessage, int statusCode = 500)
        {
            return new ServiceResponse<T> { ErrorMessage = errorMessage, IsSuccess = false, StatusCode = statusCode };
        }
    }
}
