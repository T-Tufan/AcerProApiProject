using Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IUserService
    {
        Task Create(UserCreateDto dto);
        Task<string> SignIn(UserSignInDto dto);
        Task SignOut();
    }
}
