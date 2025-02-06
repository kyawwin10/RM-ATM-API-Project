using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class AddProductDTO
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public decimal? ProfitPerItem { get; set; }
    }

    public class ProductResponseDTO
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public decimal? ProfitPerItem { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class UpdateProductDTO 
    { 
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public decimal? ProfitPerItem { get; set; }
        public string? UpdatedBy { get; set; }
    }
    //public class EditProductDTO
    //{
    //    public Guid ProductID { get; set; }
    //    public string ProductName { get; set; }
    //    public decimal Price { get; set; }
    //    public int Quantity { get; set; }
    //    public string UpdatedBy { get; set; }
    //}

    public class DeleteProductDTO
    {
        public Guid ProductID { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
