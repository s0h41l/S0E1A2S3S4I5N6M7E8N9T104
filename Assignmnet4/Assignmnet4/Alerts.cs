using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignmnet4
{
    public class Alerts
    {
        public static string alert = null;
        public static int person_id = 0;
        public static List<Person> person = null;


        public static int numberOfContects(int person_id = 22)
        {
            DB_Entities db = new DB_Entities();
            int count = 0;
            var contacts = db.Contacts.Where(x => x.PersonId == person_id);
            foreach (var i in contacts)
            {
                count++;
            }

            return count;

        }

        


    }








    
}