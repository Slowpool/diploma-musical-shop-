using DataLayer.Models;
using ServiceLayer.AdminServices.Dto;

namespace ServiceLayer.AdminServices;
public interface IUserService
{
    Task<AppUser?> GetUserInfo(Guid userId);
    Task<bool> UpdateUser(UpdateUserDto dto);
}
