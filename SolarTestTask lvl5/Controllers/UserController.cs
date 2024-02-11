using Microsoft.AspNetCore.Mvc;
using SolarTestTask_lvl5.AppData.Contexts;
using SolarTestTask_lvl5.Contracts.User;
using System.Net;


namespace Doska.API.Controllers
{
    //[ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filePath"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("/Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody]CreateUserRequest request,IFormFile filePath, CancellationToken token)
        {
            byte[] photo;

            if (filePath == null || filePath.Length == 0)
                photo = new byte[0];
            else
            {
                await using (var ms = new MemoryStream())
                await using (var fs = filePath.OpenReadStream())
                {
                    await fs.CopyToAsync(ms);
                    photo = ms.ToArray();
                }
            }

            var user = await _userService.CreateUserAsync(request,photo,token);

            return Created("",user);
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/allusers")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet("/getById")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id,CancellationToken cancellation)
        {
            var result = await _userService.GetByIdAsync(id,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Обновление ифнормации о пользователе
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="request">Новые данные</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpPut("/updateUser/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser(Guid id, EditUserRequest request, CancellationToken cancellation)
        {
            var result = await _userService.EditUserAsync(id, request,cancellation);

            return Ok(result);
        }

        
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpDelete("/deleteUser/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellation)
        {
            await _userService.DeleteAsync(id,cancellation);
            return Ok();
        }

        
    }
}
