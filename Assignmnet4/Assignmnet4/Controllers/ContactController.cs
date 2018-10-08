using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignmnet4.Models;

namespace Assignmnet4.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonContacts(int id)
        {
            DB_Entities db = new DB_Entities();
            List<Contact> contacts = db.Contacts.Where(x=>x.PersonId==id).ToList();
            List<ContactViewModel> cv = new List<ContactViewModel>();
            foreach (Contact i in contacts)
            {
                ContactViewModel con = new ContactViewModel()
                {
                    ContactNumber=i.ContactNumber,
                    Type=i.Type,
                    PersonId=i.PersonId,
                    ContactId=i.ContactId


                };
                ViewData["person_id"] = i.PersonId;
                cv.Add(con);

            }
            Alerts.person_id = id;


            return View(cv);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Create(ContactViewModel collection,int id)
        {
            

            try
            {
                DB_Entities db = new DB_Entities();
                Contact con = new Contact()
                {
                    ContactNumber = collection.ContactNumber,
                    PersonId = id,
                    Type=collection.Type,
                };
                
                db.Contacts.Add(con);
                var person = db.People.Where(x => x.PersonId == collection.PersonId).First();
                
                person.UpdateOn = DateTime.Now;
                db.SaveChanges();
                Alerts.alert = "contact_added";

                return Redirect(string.Format("~/Contact/PersonContacts/{0}",id.ToString()));
            }
            catch
            {
                Alerts.alert = "contact_failed_to_add";
                return Redirect(string.Format("~/Contact/PersonContacts/{0}", id.ToString()));
            }
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Delete/5
       

        public ActionResult Delete(int id,int person_id)
        {
           
            DB_Entities db = new DB_Entities();

            Contact con = new Contact()
            {
                ContactId = id
            };

            db.Entry(con).State = System.Data.Entity.EntityState.Deleted;
            var person = db.People.Where(x => x.PersonId == person_id).First();
            person.UpdateOn = DateTime.Now;
            db.SaveChanges();
            db.SaveChanges();
            Alerts.alert = "contact_delete";
            return Redirect(string.Format("~/Contact/PersonContacts/{0}",person_id));
            
        }



        // POST: Contact/Delete/5
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
    }
}
