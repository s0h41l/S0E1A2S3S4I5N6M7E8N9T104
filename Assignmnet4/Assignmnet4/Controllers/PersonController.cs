﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignmnet4.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Data.Entity;

namespace Assignmnet4.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId().ToString();
                DB_Entities db = new DB_Entities();
                List<Person> pvm = new List<Person>();
                var pr= db.People.Where(x => x.AddedBy == id);

                foreach (var i in pr)
                {
                    Person p = new Person()
                    {
                        
                        PersonId=i.PersonId,
                        FirstName = i.FirstName,
                        MiddleName = i.MiddleName,
                        LastName = i.LastName,
                        DateOfBirth=i.DateOfBirth,
                        AddedOn=i.AddedOn,
                        AddedBy=i.AddedBy,
                        HomeAddress=i.HomeAddress,
                        HomeCity=i.HomeCity,
                        ImagePath=i.ImagePath,
                        EmailId=i.EmailId

                    };
                    pvm.Add(p);
                }


                return View(pr);
      
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            DB_Entities db = new DB_Entities();
            var person = db.People.Where(x => x.PersonId == id);
            List<Person> p = new List<Person>();
            
            foreach(var i in person)
            {
                var pp = new Person()
                {
                    FirstName = i.FirstName,
                    MiddleName = i.MiddleName,
                    LastName = i.LastName,
                    DateOfBirth = i.DateOfBirth,
                    EmailId = i.EmailId,
                    HomeAddress = i.HomeAddress,
                    ImagePath = i.ImagePath,
                    HomeCity = i.HomeCity,


                };
                p.Add(pp);
            }
            ViewData["f"] = p ;


            return View();
        }

        // GET: Person/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(PersonViewModel collection)
        {
            
      

        
            try
            {

                string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
                string ext = Path.GetExtension(collection.Image.FileName);
                filename = filename + DateTime.Now.Millisecond.ToString();
                filename = filename + ext;
                string filetodb= "/Image/" + filename;
                filename = Path.Combine(Server.MapPath("~/Image/"), filename);

                collection.Image.SaveAs(filename);
                collection.ImagePath = filetodb;
                Person pr = new Person()
                {
                    FirstName = collection.FirstName,
                    MiddleName = collection.MiddleName,
                    LastName = collection.LastName,
                    DateOfBirth = collection.DateOfBirth,
                    HomeAddress = collection.HomeAddress,
                    HomeCity = collection.HomeCity,
                    EmailId = collection.EmailId,
                    AddedOn = DateTime.Now,
                    AddedBy= User.Identity.GetUserId(),
                    ImagePath=collection.ImagePath,
                    UpdateOn=DateTime.Now,
                   
                };

                Contact con = new Contact()
                {
                    ContactNumber = collection.Number,
                    Type = collection.type
                };
              

               
                using (DB_Entities db=new DB_Entities())
                {
                    db.People.Add(pr);
                    db.Contacts.Add(con);
                    db.SaveChanges();
                    Alerts.alert = "person_added";
                    return RedirectToAction("Index");

                }

            }
            catch
            {
                return Content("Failed");
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            DB_Entities db = new DB_Entities();
            var person = db.People.Where(x => x.PersonId == id);
            List<Person> p = new List<Person>();

            foreach (var i in person)
            {
                var pp = new Person()
                {
                    FirstName = i.FirstName,
                    MiddleName = i.MiddleName,
                    LastName = i.LastName,
                    DateOfBirth = i.DateOfBirth,
                    EmailId = i.EmailId,
                    HomeAddress = i.HomeAddress,
                    ImagePath = i.ImagePath,
                    HomeCity = i.HomeCity,


                };
                p.Add(pp);
            }
            ViewData["ff"] = p;


            return View();
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonViewModel collection)
        {

            try
            {

                DB_Entities db = new DB_Entities();
                var person = db.People.Where(x => x.PersonId == id).First();
                person.FirstName = collection.FirstName;
                person.MiddleName = collection.MiddleName;
                person.LastName = collection.LastName;
                person.DateOfBirth = collection.DateOfBirth;
                person.EmailId = collection.EmailId;
                person.HomeAddress = collection.HomeAddress;
                person.HomeCity = collection.HomeCity;
                person.UpdateOn = DateTime.Now;
                string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
                string ext = Path.GetExtension(collection.Image.FileName);
                filename = filename + DateTime.Now.Millisecond.ToString();
                filename = filename + ext;
                string filetodb = "/Image/" + filename;
                filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                collection.Image.SaveAs(filename);
                person.ImagePath = filetodb;
                person.UpdateOn = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            DB_Entities db = new DB_Entities();

            List<Contact> c = db.Contacts.Where(x => x.PersonId == id).ToList();
            foreach(var i in c)
            {
                db.Entry(i).State = EntityState.Deleted;
                db.SaveChanges();
            }


            var p = new Person()
            {
                PersonId = id,
            };
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            Alerts.alert = "person_deleted";
            return Redirect("/Person/Index");
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult numberOfContects(int id)
        {
            DB_Entities db = new DB_Entities();
            var count = 0;
         
            var contacts = db.Contacts.Where(x => x.PersonId == id);
            foreach(var i in contacts)
            {
                count++;
            }

            return Json(count, JsonRequestBehavior.AllowGet);

        }


        


    }
    


    
}
