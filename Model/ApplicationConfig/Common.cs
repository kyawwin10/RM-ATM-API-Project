using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.ApplicationConfig
{
    public class Common
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set;} = DateTime.UtcNow;
        public string? CreatedBy { get; set; } = "Admin";
        public string? UpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
