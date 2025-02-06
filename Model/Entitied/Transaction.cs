using MODEL.ApplicationConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entitied
{
    public class Transaction
    {
        [Key]
        public Guid TransactionID { get; set; }
        public Guid UserID { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
