using MODEL.ApplicationConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entitied
{
    public class Sale
    {
        public Guid SaleID {  get; set; }
        public Guid ProductID { get; set; }
        public int? QuantitySold { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalProfit { get; set; }
        public DateTime SalesDate { get; set; }
    }
}
