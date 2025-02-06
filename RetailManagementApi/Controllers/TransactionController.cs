using Asp.Versioning;
using BAL.IServices;
using BAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTO;
using REPOSITORY.UnitOfWork;

namespace RetailManagementApi.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;
        public TransactionController(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
        }


        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(WithDrawDTO inputModel)
        {
            if (inputModel == null)
            {
                return Ok(new ResponseModel { Message = "Invalid input", Status = APIStatus.Error });
            }

            try
            {
                var response = await _transactionService.WithDraw(inputModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }


        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(DepositDTO inputModel)
        {
            try
            {
                await _transactionService.Deposit(inputModel);
                return Ok(new ResponseModel { Message = "Deposit Processed Successfully", Status = APIStatus.Success });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }
    };


}
