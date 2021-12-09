using Microsoft.AspNetCore.Mvc;

namespace StorePractice.Controllers
{
    public class CrudRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
