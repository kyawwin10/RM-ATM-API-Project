using BAL.IServices;
using MODEL.DTO;
using MODEL.Entitied;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class SaleService: ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public async Task<bool> SaleProduct(IEnumerable<SaleProductDTO> inputModel)
        {
            if (inputModel == null || !inputModel.Any())
            {
                return false;
            }

                
                foreach (var saleRequest in inputModel)
                {
                    var product = (await _unitOfWork.Products.GetByCondition(x => x.ProductID == saleRequest.ProductID && x.ActiveFlag)).FirstOrDefault();

                    if (product == null || !product.ActiveFlag || product.Stock < saleRequest.QuantitySold)
                    {
                        return false;
                    }


                    var Total = product.Price * saleRequest.QuantitySold;
                var totalProfit = product.ProfitPerItem * saleRequest.QuantitySold;


                var sale = new Sale
                    {
                        SaleID = Guid.NewGuid(),
                        ProductID = saleRequest.ProductID,
                        QuantitySold = saleRequest.QuantitySold,
                        TotalAmount = Total,
                        TotalProfit = totalProfit,
                        SalesDate = DateTime.UtcNow
                };

                   await _unitOfWork.Sales.Add(sale);
                  
                    product.Stock -= saleRequest.QuantitySold;
                    _unitOfWork.Products.Update(product);
                }
                await _unitOfWork.SaveChangeAsync();
                return true;
        }

        public async Task<SalesReportDTO> SalesReport()
        {
            var sales = await _unitOfWork.Sales.GetAll();
            if (sales == null || !sales.Any())
            {
                throw new Exception("No Sale Record");
            }

            var salesReport = new SalesReportDTO();
            foreach (var sale in sales)
            {
                salesReport.TotalPrice += sale.TotalAmount ?? 0;
                salesReport.TotalProfit += sale.TotalProfit ?? 0;
            }

            return salesReport;
        }


        public async Task<IEnumerable<Sale>> GetSaleById(Guid Id)
        {
            try
            {
                var sales = await _unitOfWork.Sales.GetByCondition(x => x.SaleID == Id);
                return sales;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
