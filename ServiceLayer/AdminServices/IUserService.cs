using BusinessLogic.AdminPanel.Dto;
using DataLayer.Models;

namespace ServiceLayer.AdminServices;
public interface IUserService
{
    Task<AppUser?> GetUserInfo(Guid userId);
    Task<bool> UpdateUser(UpdateUserDto dto);
}
