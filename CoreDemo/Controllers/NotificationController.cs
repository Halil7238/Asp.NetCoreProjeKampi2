using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class NotificationControler : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
