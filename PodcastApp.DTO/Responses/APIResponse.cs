using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastApp.DTO.Responses
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }

        public APIResponse(T data, string message = "Success")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public APIResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
        }
    }
}
