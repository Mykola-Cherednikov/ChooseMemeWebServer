using ChooseMemeWebServer.Application.DTO.UserService.Request;
using ChooseMemeWebServer.Application.DTO.UserService.Response;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseDTO> Login(UserRequestDTO data);

        public Task<UserResponseDTO> Register(UserRequestDTO data);
    }
}
