using MODEL.DTO;
using MODEL.Entitied;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface ISaleService
    {
        Task<bool> SaleProduct(IEnumerable<SaleProductDTO> inputModel);
        Task<IEnumerable<Sale>> GetSaleById(Guid Id);
        Task<SalesReportDTO> SalesReport();
    }
}