using Marketplace.Domain.Entity;
using Marketplace.Domain.Enum;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Account;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Domain.ViewModels.User;
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

        IBaseResponse<List<Product>> Search(string searchString);

        Task<IBaseResponse<ProductViewModel>> GetProduct(long id);

		IBaseResponse<List<Product>> GetProductsByCategory(string category);

		Task<BaseResponse<Dictionary<long, string>>> GetProduct(string term);

        Task<IBaseResponse<Product>> Create(ProductViewModel model, byte[] imageData, string ownerName);

        Task<IBaseResponse<bool>> DeleteProduct(long id, string ownerName);

        Task<IBaseResponse<Product>> Edit(long id, ProductViewModel model);

        Task<BaseResponse<bool>> AddBid(Bid bid, string BidNameUser);
    }
}
