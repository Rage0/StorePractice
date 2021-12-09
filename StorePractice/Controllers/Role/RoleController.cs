using Microsoft.AspNetCore.Mvc;

namespace StorePractice.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
