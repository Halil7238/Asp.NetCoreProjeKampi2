using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Notification
{
    public class NotificationList : ViewComponent
    {
        NotificationManager Nm = new NotificationManager(new EfNotificationRepository());

        public IViewComponentResult Invoke()
        {
            var values = Nm.GetList();
            return View(values);
        }

    }
}
