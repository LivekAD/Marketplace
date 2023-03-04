using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Interfaces
{
    public interface IProductService
    {
        BaseResponse<Dictionary<int, string>> GetCategory();

        BaseResponse<Dictionary<int, string>> GetSubCategory();

        IBaseResponse<List<Product>> GetProducts();

        Task<IBaseResponse<ProductViewModel>> GetProduct(long id);

        Task<BaseResponse<Dictionary<long, string>>> GetProduct(string term);

        Task<IBaseResponse<Product>> Create(ProductViewModel model, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteProduct(long id);

        Task<IBaseResponse<Product>> Edit(long id, ProductViewModel model);
    }
}
