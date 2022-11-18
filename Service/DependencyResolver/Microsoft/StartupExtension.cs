using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using DataAccess.UnitOfWork;
using DataAccess.IRepositories;
using Services.IService;
using Services.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Mappings;
using AutoMapper;
using Entities.Entity;
using Services.CustomDescriber;
using Microsoft.AspNetCore.Http;

namespace Service.DependencyResolver.Microsoft
{
    public static class StartupExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                //opt.Password.RequireDigit = false;
                //Identity için varsayılan veri kontrol durumları güncellenir.
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                //opt.SignIn.RequireConfirmedEmail = false;
            }).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<Context>();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.Cookie.Name = "AcerSoftCookie";
                opt.ExpireTimeSpan = TimeSpan.FromDays(25); // 25 gün boyunca kullanıcı bilgisi cookie de saklanır.
                //Hata durumlarında frontend tarafı için route yapılır.
                //opt.LoginPath = new PathString("/Home/SignIn");
                //opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
            });

            services.AddDbContext<Context>(opt =>
            {
                opt.UseSqlServer("server = LAPTOP-2J88RUNF; database = AcerProDb; integrated security = true;");
                opt.LogTo(Console.WriteLine, LogLevel.Information);

            });
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new NewsProfile());
                opt.AddProfile(new UserProfile());
            });
            var mapper = configuration.CreateMapper();

            services.AddSingleton<IMapper>(mapper);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
