using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Profile;
using Marketplace.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel model);

        BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        Task<BaseResponse<UserViewModel>> GetUser(string userName);

        Task<IBaseResponse<bool>> DeleteUser(long id);
    }
}
