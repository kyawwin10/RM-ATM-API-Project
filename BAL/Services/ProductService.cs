using BAL.IServices;
using MODEL.DTO;
using MODEL.Entitied;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddProduct(AddProductDTO inputModel)
        {
            try
            {
                //var productProfit = (inputModel.Price - inputModel.CostPrice) * inputModel.Quantity; 
                var addproduct = new Product()
                {
                    ProductName = inputModel.ProductName,
                    Price = inputModel.Price,
                    Stock = inputModel.Stock,
                    ProfitPerItem = inputModel.ProfitPerItem
                };
                await _unitOfWork.Products.Add(addproduct);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductResponseDTO> GetProductById(Guid productID)
        {
            try
            {
                var product_data = (await _unitOfWork.Products.GetByCondition(x => x.ProductID == productID && x.ActiveFlag)).FirstOrDefault();
                if (product_data == null)
                {
                    throw new KeyNotFoundException($"Product with ID {productID} was not found.");
                }
                return new ProductResponseDTO
                {
                    ProductName = product_data.ProductName,
                    Price = product_data.Price,
                    Stock = product_data.Stock,
                    ProfitPerItem = product_data.ProfitPerItem,
                    CreatedBy = product_data.CreatedBy
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the product details.", ex);
            }
        }




        public async Task UpdateProduct(UpdateProductDTO inputModel)
        {
            try
            {
                //var productProfit = (inputModel.Price - inputModel.CostPrice) * inputModel.Quantity;
                var product_data = (await _unitOfWork.Products.GetByCondition(x => x.ProductID == inputModel.ProductID && x.ActiveFlag)).FirstOrDefault();
                if (product_data != null)
                {
                    product_data.ProductName = inputModel.ProductName;
                    product_data.Price = inputModel.Price;
                    product_data.Stock = inputModel.Stock;
                    product_data.ProfitPerItem = inputModel.ProfitPerItem;
                    product_data.UpdatedBy = "Admin";
                    product_data.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.Products.Update(product_data);
                }
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task DeleteProduct(DeleteProductDTO inputModel)
        //{
        //    try
        //    {
        //        var product_data = (await _unitOfWork.Tbl_Products.GetByCondition(x => x.ProductID == inputModel.ProductID)).FirstOrDefault();

        //        if (product_data == null || !product_data.ActiveFlag)
        //        {
        //            throw new Exception("Product not found.");
        //        }
        //        product_data.ActiveFlag = false;
        //        product_data.UpdatedBy = inputModel.UpdatedBy;
        //        product_data.UpdatedDate = DateTime.UtcNow;
        //        _unitOfWork.Tbl_Products.Update(product_data);
        //        await _unitOfWork.SaveChangeAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while deleting the product.", ex);
        //    }
        //}

        public async Task DeleteProduct(DeleteProductDTO inputModel)
        {
            try
            {
                var product_data = (await _unitOfWork.Products.GetByCondition(x => x.ProductID == inputModel.ProductID && x.ActiveFlag)).FirstOrDefault();

                // Check if the product exists
                if (product_data == null)
                {
                    throw new Exception("Product not found.");
                }

                // Mark the product as inactive
                product_data.ActiveFlag = false;
                product_data.UpdatedBy = "Admin";
                product_data.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Products.Update(product_data);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                // Log the error if logging is implemented
                throw new Exception("An error occurred while processing the delete operation. " + ex.Message, ex);
            }
        }



    }
}
