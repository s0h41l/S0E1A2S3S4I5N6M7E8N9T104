using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignmnet4.Models;

namespace Assignmnet4.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        
        // GET: DashBoard
        
        public ActionResult Index()
        {
            List<DateTime> birthdaydays = BirthDays();
            List<DateTime> lastSevenDays = LastSevenDays();

            List<PersonViewModel> birthdayBoys = new List<PersonViewModel>();
            DB_Entities db = new DB_Entities();
            var persons = db.People;
     
            foreach(var i in persons)
            {
                foreach(DateTime j in birthdaydays)
                {
                    if(j.Day==Convert.ToDateTime(i.DateOfBirth).Day && j.Month == Convert.ToDateTime(i.DateOfBirth).Month)
                    {
                        PersonViewModel p = new PersonViewModel() {
                        
                            FirstName=i.FirstName,
                            MiddleName=i.MiddleName,
                            LastName=i.LastName,
                            PersonId=i.PersonId,
                            EmailId=i.EmailId,
                            ImagePath=i.ImagePath,
                            

                        };
                        birthdayBoys.Add(p);
                    }
                }
            }

            List<Person> updatedPerson = new List<Person>();


            foreach (var i in persons)
            {
                foreach (DateTime j in lastSevenDays)
                {
                    if (j.Day == Convert.ToDateTime(i.UpdateOn).Day && j.Month == Convert.ToDateTime(i.UpdateOn).Month)
                    {
                        Person p = new Person()
                        {

                            FirstName = i.FirstName,
                            MiddleName = i.MiddleName,
                            LastName = i.LastName,
                            PersonId = i.PersonId,
                            EmailId = i.EmailId,
                            ImagePath = i.ImagePath,


                        };
                        updatedPerson.Add(p);
                    }
                }
            }

            Alerts.person = updatedPerson;




            return View(birthdayBoys);
        }

        // GET: DashBoard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashBoard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashBoard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBoard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashBoard/Edit/5
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

        // GET: DashBoard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashBoard/Delete/5
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

        public List<DateTime> BirthDays()
        {

            List<DateTime> daterange = new List<DateTime>();
            for (int i = 1; i < 11; i++)
            {
                DateTime t = DateTime.Now;
                daterange.Add(t.AddDays(i).Date);



            }
            DateTime today = DateTime.Now;
            daterange.Add(today.Date);
            return daterange;

        }

        public List<DateTime> LastSevenDays()
        {

            List<DateTime> daterange = new List<DateTime>();
            for (int i = -6; i < 0; i++)
            {
                DateTime t = DateTime.Now;
                daterange.Add(t.AddDays(i).Date);


            }
            DateTime today = DateTime.Now;
            daterange.Add(today.Date);

            return daterange;
        }


    }
}
