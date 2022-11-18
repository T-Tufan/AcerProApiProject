using Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto model)
        {
            //Kullanıcı eklemek için kullacıdan alınan model servise gönderilir.
            var result = _userService.Create(model);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInDto model)
        {
            //Giriş yapmak için kullacıdan alınan model servise gönderilir.
            var result = await _userService.SignIn(model);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            //Kullanıcının çıkış yapması sağlanır. Cookie bilgileri silinir.
            var result = _userService.SignOut();
            return Ok(result);
        }
    }
}
