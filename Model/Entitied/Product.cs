using MODEL.ApplicationConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entitied
{
    public class Product:Common
    {
        [Key]
        public Guid ProductID {get; set;}
        public string? ProductName { get; set;}
        public decimal? Price { get; set;}
        public int? Stock { get; set;}
        public decimal? ProfitPerItem { get; set;}
    }
}
