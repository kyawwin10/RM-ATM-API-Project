using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class WithDrawDTO
    {
        public Guid UserID { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class DepositDTO
    {
        public Guid UserID { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
