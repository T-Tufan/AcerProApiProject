using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class NewsController : Controller
    {
        [HttpGet]
        public IActionResult AllNews()
        {
            return View();
        }
    }
}
