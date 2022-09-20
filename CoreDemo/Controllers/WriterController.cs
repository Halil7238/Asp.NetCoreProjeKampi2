using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessLayer.Concrete;
using CoreDemo.Models;
using System.IO;
using FluentValidation;

namespace CoreDemo.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        Context c = new Context();

        public IActionResult Dashboard()
        {
            ViewBag.BlogCount = c.Blogs.Count();
            ViewBag.BlogByWriterCount = c.Blogs.Where(x => x.WriterId == 1).Count();
            ViewBag.CategoryCount = c.Categories.Count();
            //var values = bm.GetList();
            return View(/*values*/);
        }
        public IActionResult BlogListByWriter()
        {
            var values = bm.GetListWithCategoryByWriterBM(1);
            return View(values);
        }

        public PartialViewResult NavbarMenuTop()
        {
            return PartialView();
        }
        public PartialViewResult NavbarMenuLeft()
        {
            return PartialView();
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {

            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategroyName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(blog);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategroyName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            if (results.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreadDate = DateTime.Parse(DateTime.Now.ToString());
                blog.WriterId = 1;
                bm.Add(blog);
                return RedirectToAction("Dashboard", "Writer");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View();
        }
        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.GetByID(id);
            bm.Delete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult BlogUpdate(int id)
        {
            var blogvalue = bm.GetByID(id);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategroyName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult BlogUpdate(Blog blog)
        {

            bm.Update(blog);
            return RedirectToAction("BlogListByWriter");

        }

        [HttpGet]
        public IActionResult WriterUpdate()
        {
            var values = wm.GetByID(1);
            List<SelectListItem> writercityvalues = (from x in wm.GetList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x.WriterCity,
                                                         Value = x.WriterCity.ToString(),
                                                     }).ToList();
            ViewBag.wv = writercityvalues;

            return View(values);
        }
        [HttpPost]
        public IActionResult WriterUpdate(Writer writer)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(writer);
            if (results.IsValid)
            {
                List<SelectListItem> writercityvalues = (from x in wm.GetList()
                                                         select new SelectListItem
                                                         {
                                                             Text = x.WriterCity,
                                                             Value = x.WriterCity.ToString(),
                                                         }).ToList();
                ViewBag.wv = writercityvalues;
                writer.WriterPassword = BCrypt.Net.BCrypt.HashPassword(writer.WriterPassword);
                writer.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(writer.ConfirmPassword);//Şifre Hash(Gizleme)
                wm.Update(writer);
                return RedirectToAction("Dashboard", "Writer");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View();

        }
        [HttpGet]
        public IActionResult WriterAdd()
        {
            List<SelectListItem> writercityvalues = (from x in wm.GetList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x.WriterCity,
                                                         Value = x.WriterCity.ToString(),
                                                     }).ToList();
            ViewBag.wv = writercityvalues;
            return View();
        }

        [HttpPost]
        public IActionResult WriterAdd(AddWriterImage writer)
        {
            Writer w = new Writer();
            if (writer.WriterImage != null)
            {
                var extension = Path.GetExtension(writer.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                writer.WriterImage.CopyTo(stream);
                w.WriterImage = newimagename;
            }
            w.WriterMail = writer.WriterMail;
            w.WriterName = writer.WriterName;
            w.WriterPassword = writer.WriterPassword;
            w.ConfirmPassword = writer.ConfirmPassword;
            w.WriterStatus = writer.WriterStatus;
            w.WriterAbout = writer.WriterAbout;
            wm.Add(w);
            return RedirectToAction("Dashboard", "Writer");



            //WriterValidator wv = new WriterValidator();
            //ValidationResult results = wv.Validate(writer);
            //if (results.IsValid)
            //{

            //    writer.WriterPassword = BCrypt.Net.BCrypt.HashPassword(writer.WriterPassword);
            //    writer.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(writer.ConfirmPassword);//Şifre Hash(Gizleme)
            //    wm.Update(writer);
            //    return RedirectToAction("BlogListByWriter");
            //}
            //else
            //{
            //    foreach (var item in results.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

            //    }
            //}
            //return View();
        }

        public IActionResult WriterProfile()
        {
            var values = wm.GetByID(1);
            return View(values);

        }


    }
}
