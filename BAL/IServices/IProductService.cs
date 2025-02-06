using MODEL.DTO;
using MODEL.Entitied;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IProductService
    {
        Task AddProduct(AddProductDTO inputModel);
        Task<ProductResponseDTO> GetProductById(Guid ProductID);
        Task UpdateProduct(UpdateProductDTO inputModel);
        Task DeleteProduct(DeleteProductDTO inputModel);

    }
}
