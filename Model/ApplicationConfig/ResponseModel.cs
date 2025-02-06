using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.ApplicationConfig
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public APIStatus Status { get; set; }
        public object? Data { get; set; }
    }
    public enum APIStatus
    {
        Success = 0,
        Error = 1,
        SystemError = 2,
    }
}
