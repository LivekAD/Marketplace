using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Extensions;
using Marketplace.Domain.Entity;
using Marketplace.Domain.Enum;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Marketplace.Domain.ViewModels.User;
using Marketplace.Domain.Helpers;

namespace Marketplace.Service.Implementations
{
    public class ProductService : IProductService
    {

        #region Include Data Base

        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ProductPhoto> _productPhotoRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Bid> _bidRepository;

        public ProductService(IBaseRepository<Product> productRepository, IBaseRepository<User> userRepository, IBaseRepository<Bid> bidRepository, IBaseRepository<ProductPhoto> productPhotoRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _bidRepository = bidRepository;
            _productPhotoRepository = productPhotoRepository;
        }

        #endregion

        #region Add Bid

        public async Task<BaseResponse<bool>> AddBid(Bid bid, string BidNameUser)
        {
            try
            {                
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Name == bid.ProductName);
                if(product == null || product.isAuction != "true")
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Аукціон не знайдений"
                    };
                }

                bid.AuctionId = product.Id;
                bid.BidUserName = BidNameUser;
                product.Price = product.Price + bid.lastBid;
                bid.BidAmount = product.Price;
                await _productRepository.Update(product);

                await _bidRepository.Create(bid);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Ставка прийнята"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Create

        public async Task<IBaseResponse<Product>> Create(ProductViewModel model, List<ProductPhoto> imageData, string ownerName)
        {
            try
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    DateCreate = DateTime.Now,
                    Category = (Category)Convert.ToInt32(model.Category),
                    SubCategory = (SubCategory)Convert.ToInt32(model.SubCategory),
                    Price = model.Price,
                    MainPhoto = imageData[0].ImageData,
                    Photo = imageData,
                    OwnerName = ownerName,
                    isAuction = model.isAuction,
                    EndingAuction = model.EndingAuction,
                };
                await _productRepository.Create(product);

                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.OK,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Delete Product

        public async Task<IBaseResponse<bool>> DeleteProduct(long id, string ownerName)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                if(product.OwnerName == ownerName)
                {
                    await _productRepository.Delete(product);
                }                

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Edit

        public async Task<IBaseResponse<Product>> Edit(long id, ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Product not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                product.Description = model.Description;
                product.Price = model.Price;
                product.DateCreate = DateTime.ParseExact(model.DateCreate, "yyyyMMdd HH:mm", null);
                product.Name = model.Name;

                await _productRepository.Update(product);


                return new BaseResponse<Product>()
                {
                    Data = product,
                    StatusCode = StatusCode.OK,
                };
                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Get Category

        public BaseResponse<Dictionary<int, string>> GetCategory()
        {
            try
            {
                var categories = ((Category[])Enum.GetValues(typeof(Category)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = categories,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Get Product Id

        public async Task<IBaseResponse<ProductViewModel>> GetProduct(long id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var photo = await _productPhotoRepository.GetAll().Where(x => x.ProductId == id).ToListAsync();
                if (product == null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        Description = "Користувач не знайдений",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new ProductViewModel()
                {
                    Id = id,
                    DateCreate = product.DateCreate.ToLongDateString(),
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category.GetDisplayName(),
                    SubCategory = product.SubCategory.GetDisplayName(),
                    Image = product.Photo,
                    OwnerName = product.OwnerName,
                    ChatMessages = product.ChatMessages
                };

                return new BaseResponse<ProductViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Get Product Term

        public async Task<BaseResponse<Dictionary<long, string>>> GetProduct(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var product = await _productRepository.GetAll()
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        DateCreate = x.DateCreate.ToLongDateString(),
                        Price = x.Price,
                        Category = x.Category.GetDisplayName(),
                        SubCategory = x.SubCategory.GetDisplayName()
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = product;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

		#endregion

		#region Get Product by category

		public IBaseResponse<List<Product>> GetProductsByCategory(string category)
		{
			try
			{
				var products = _productRepository.GetAll().AsEnumerable().Where(x => x.Category.ToString() == category).ToList();
				if (!products.Any())
				{
					return new BaseResponse<List<Product>>()
					{
						Description = "Знайдено 0 елементів",
						StatusCode = StatusCode.OK
					};
				}

				return new BaseResponse<List<Product>>()
				{
					Data = products,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Product>>()
				{
					Description = $"[GetProducts] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		#endregion

		#region Get Products

		public IBaseResponse<List<Product>> GetProducts()
        {
            try
            {
                var products = _productRepository.GetAll().ToList();
                var photo = _productPhotoRepository.GetAll().ToList();
                if (!products.Any())
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Знайдено 0 елементів",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Get Sub Category

        public BaseResponse<Dictionary<int, string>> GetSubCategory()
        {
            try
            {
                var subCategories = ((SubCategory[])Enum.GetValues(typeof(SubCategory)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = subCategories,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

        #region Search

        public IBaseResponse<List<Product>> Search(string searchString)
        {
            try
            {
                var products = from m in _productRepository.GetAll() select m;
                products.Where(s => s.Name!.Contains(searchString));
                if (!products.Any())
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Знайдено 0 елементів",
                        StatusCode = StatusCode.OK
                    };
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Помилка вводу",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Product>>()
                {
                    Data = products.ToList(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        #endregion

    }
}
