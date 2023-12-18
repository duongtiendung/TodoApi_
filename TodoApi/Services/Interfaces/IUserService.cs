using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUser(LoginRequest request);
        Task<LoginResponse> LoginUser(LoginRequest request);
    }
}
