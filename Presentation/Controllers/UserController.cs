using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.ApiHandlers;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _urlConfig;

        public UserController(IApiHandler apiHandler, IConfiguration urlConfig)
        {
            _apiHandler = apiHandler;
            _urlConfig = urlConfig;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginProcess(UserSignInModel userSignInModel)
        {
            var loginUrl = _urlConfig["BaseURL"] + UrlStrings.LoginUrl;
            var state = _apiHandler.PostApiString(userSignInModel, loginUrl);
            if (state == "Giriş başarılı")
            {
                return RedirectToAction("AllNews", "News");
            }
            return View("Login");
        }
    }
}
