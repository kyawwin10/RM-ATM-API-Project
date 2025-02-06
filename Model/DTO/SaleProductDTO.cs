using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class SaleProductDTO
    {
        public Guid ProductID { get; set; }
        public int QuantitySold { get; set; }
        public DateTime SalesDate { get; set; }
    }
    public class SalesReportDTO
    {
        public decimal? TotalPrice { get; set; } = 0;
        public decimal? TotalProfit { get; set; } = 0;
    }
}
