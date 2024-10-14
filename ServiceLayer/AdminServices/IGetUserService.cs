using BusinessLogic.AdminPanel.Dto;
using DataLayer.Models;

namespace ServiceLayer.AdminServices;
public interface IGetUserService
{
    Task<AppUser?> GetUserInfo(Guid userId);
}
