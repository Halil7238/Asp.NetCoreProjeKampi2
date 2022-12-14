using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NotificationManager : INotificationService
    {
        INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public void Add(Notification t)
        {
            _notificationDal.Insert(t);
        }

        public void Delete(Notification t)
        {
            _notificationDal.Delete(t);
        }

        public Notification GetByID(int id)
        {
            return _notificationDal.GetById(id);
        }

        public List<Notification> GetList()
        {
            return _notificationDal.GetListAll().Where(x => x.NotificationStatus == true).OrderByDescending(x => x.NotificationDate).Take(6).ToList();
        }

        public void Update(Notification t)
        {
            _notificationDal.Update(t);
        }
    }
}
