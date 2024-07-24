using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Web.Controllers
{
    public class AdminUserController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
