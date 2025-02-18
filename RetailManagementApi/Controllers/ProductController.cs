using Asp.Versioning;
using BAL.IServices;
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
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public ProductController(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var productdata = await _unitOfWork.Products.GetByCondition(x => x.ActiveFlag == true);
                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Success, Data = productdata });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDTO inputModel)
        {
            try
            {
                await _productService.AddProduct(inputModel);
                return Ok(new ResponseModel { Message = "Product Create Successfully", Status = APIStatus.Success});
            }
            catch (Exception ex) { 
                return Ok(new ResponseModel {Message = ex.InnerException.Message,Status = APIStatus.Error});
            }
        }

        [HttpPatch("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                if (id != null)
                {

                    var product_data = await _productService.GetProductById(id);
                    return Ok(new ResponseModel { Message = "Product Get Successfully", Status = APIStatus.Success, Data = product_data });
                }
                else
                {
                    return Ok(new ResponseModel { Message = "Product Fail", Status = APIStatus.Error });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO inputModel)
        {
            try
            {
                if (inputModel == null)
                {
                    return Ok(new ResponseModel { Message = "Product Fail", Status = APIStatus.Success });

                }
                else
                {
                    await _productService.UpdateProduct(inputModel);
                    return Ok(new ResponseModel { Message = "Product Updated Successfully", Status = APIStatus.Success });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }


        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductDTO inputModel)
        {
            try
            {
                await _productService.DeleteProduct(inputModel);
                return Ok(new ResponseModel { Message = "Product Deleted Successfully", Status = APIStatus.Success });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }
    }
}
