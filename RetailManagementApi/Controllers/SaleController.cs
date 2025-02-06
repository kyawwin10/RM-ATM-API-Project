using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTO;
using REPOSITORY.Repositories.IRepositories;
using REPOSITORY.UnitOfWork;

namespace RetailManagementApi.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleService _saleService;
        public SaleController(IUnitOfWork unitOfWork, ISaleService saleService)
        {
            _unitOfWork = unitOfWork;
            _saleService = saleService;
        }
        [HttpPost("SellProduct")]
        public async Task<IActionResult> SaleProduct(List<SaleProductDTO> inputModel)
        {
            try
            {

                if (await _saleService.SaleProduct(inputModel))
                {
                    return Ok(new ResponseModel { Message = "Thanks for Purchasing", Status = APIStatus.Success });

                }
                else
                {
                    return Ok(new ResponseModel { Message = "Fail", Status = APIStatus.Error });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpGet("GetAllSale")]
        public async Task<IActionResult> GetAllSale()
        {
            try
            {
                var sale_data = await _unitOfWork.Sales.GetAll();
                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Success, Data = sale_data });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpGet("GetSaleById")]
        public async Task<IActionResult> GetSaleByID(Guid Id)
        {
            try
            {

                if (Id != null)
                {
                    var sale_data = await _saleService.GetSaleById(Id);
                    return Ok(new ResponseModel { Message = "Get Sale By ID Successfully", Status = APIStatus.Success, Data = sale_data });
                }
                else
                {
                    return Ok(new ResponseModel { Message = "Get Sale By ID Fail", Status = APIStatus.Error });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("SaleReport")]
        public async Task<IActionResult> SalesReport()
        {
            try
            {
                var result = await _saleService.SalesReport();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
