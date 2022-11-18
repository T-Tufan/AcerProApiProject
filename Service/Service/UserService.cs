using AutoMapper;
using DataAccess.UnitOfWork;
using Dtos.UserDtos;
using Entities.Entity;
using Microsoft.AspNetCore.Identity;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public UserService(IUnitOfWork unitofwork, IMapper mapper, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Create(UserCreateDto dto)
        {
            //Controller tarafından alınan model maplendi.
            AppUser appUser = _mapper.Map<UserCreateDto, AppUser>(dto);
            //Veritabanında member rolü varsa getirilecek.
            var memberRole = await _roleManager.FindByNameAsync("Member");
            if (memberRole == null)
            {
                //Member rolü yoksa oluşturulacak.
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    CreatedTime = DateTime.Now
                });
            }
            //Şifre hashlame yapılarak kullanıcı eklenecek.
            var identityResult = await _userManager.CreateAsync(appUser, dto.Password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "Member");
            }
            //Hatalar varsa bu alanda bir modele aktarılacak.
            foreach (var error in identityResult.Errors)
            {

            }
        }
        public async Task<string> SignIn(UserSignInDto model)
        {
            var hasUser = await _userManager.FindByNameAsync(model.UserName);
            var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
            var message = string.Empty;
            if (signInResult.Succeeded)
            {
                var userRole = await _userManager.GetRolesAsync(hasUser); // bu user bilgilerine göre kullanıcı rolü getir.
                message = $"Giriş başarılı";
                //iş başarılı
            }
            else if (signInResult.IsLockedOut)
            {
                //hesap kitli
                var lockOutEnd = await _userManager.GetLockoutEndDateAsync(hasUser);
                message = $"Hesabınız {(lockOutEnd.Value.UtcDateTime - DateTime.UtcNow).Seconds} saniye süresince askıya alınmıştır!";
            }
            else
            {

                if (hasUser != null)
                {
                    var failedCount = await _userManager.GetAccessFailedCountAsync(hasUser);
                    message = $"{_userManager.Options.Lockout.MaxFailedAccessAttempts - failedCount} kez daha hatalı girerseniz hesabınız geçici olarak kilitlenecektir!";
                }
                else
                {
                    message = "Kullanıcı adı veya şifre hatalı!";
                }
            }
            //Durumlara göre sayfalara yönlendirme saypılması gerekiyordu fakat frontend tarafı olmadığı için mesaj yazdırıldı.
            return message;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
