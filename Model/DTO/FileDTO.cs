using MODEL.ApplicationConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    //public class FileDTO
    //{
    //    public Stream? Content { get; set; }
    //    public string? FileName {  get; set; }
    //    public string? ContentType { get; set; }
    //}

    //public class FileResponseDTO
    //{
    //    public Stream? Content { get; set; }
    //    public string? FileName { get; set; }
    //    public string? ContentType { get; set; }
    //}

    //public class DeleteFileDTO
    //{
    //    public Guid FileID { get; set; }
    //    public string? UpdatedBy { get; set; }
    //}

    public class FileResponseDTO
    {
        public Stream? Content { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
    }
}
