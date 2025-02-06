using MODEL.ApplicationConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entitied
{
    public class Files:Common
    {
        [Key]
        public Guid FileID { get; set; }
        public string? FileName {  get; set; }
        public string? URL { get; set; }
        public string? ContentType { get; set; }
    }
}
