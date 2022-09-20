﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsletterManager : INewsletterService
    {
        INewsletterDal _newsletterDal;

        public NewsletterManager(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        public void Add(Newsletter newsletter)
        {
            _newsletterDal.Insert(newsletter);
        }

        public void Delete(Newsletter t)
        {
            throw new NotImplementedException();
        }

        public Newsletter GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Newsletter> GetList()
        {
            throw new NotImplementedException();
        }

        public void  Update(Newsletter t)
        {
            throw new NotImplementedException();
        }
    }
}
