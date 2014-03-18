using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ReferenceBook.Models;

namespace ReferenceBook.Controllers
{
    public class DirectoryController : Controller
    {
        private ReferenceBookEntities db=new ReferenceBookEntities();

        public ActionResult ShowData() 
        {
            try
            {
                var persons = (from person in db.Persons select person).ToList();
                //результаты выборки будут преобразованы в массив
                return View(persons);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return RedirectToAction("Index", "Home");
        }

    
        [HttpGet]
        public ActionResult AddData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddData(Person person)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    IEnumerable<Person> persons = db.Persons;
                    ViewBag.Persons = persons.ToList();
                    db.Entry(person).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("ShowData");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View(person);

           
        }
        [HttpGet]
        public ActionResult EditData(int id)
        {

            Person person = db.Persons.Find(id);

            return View(person);
        }

        [HttpPost]
        public ActionResult EditData(Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //обновляем модель
                    db.Entry(person).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ShowData");
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }

            return View(person);
        }

        public ActionResult Delete(int id)
        {

            return View(db.Persons.Find(id));

        }

        [HttpPost]
        public ActionResult Delete(int id, Person person)
        {
            try
            {

                db.Entry(person).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("ShowData");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
                return View();
            }

        }
        }


    }

