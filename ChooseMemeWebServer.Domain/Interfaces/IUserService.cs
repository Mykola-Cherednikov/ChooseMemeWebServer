using ChooseMemeWebServer.Application.DTO.UserService.Request;
using ChooseMemeWebServer.Application.DTO.UserService.Response;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseDTO> Login(UserRequestDTO data);

        public Task<UserResponseDTO> Register(UserRequestDTO data);

        public Task<User> CreateUser(string userName, string password);

        public Task<User> GetUserById(string id);

        public Task<List<User>> GetAllUsers();
    }
}
