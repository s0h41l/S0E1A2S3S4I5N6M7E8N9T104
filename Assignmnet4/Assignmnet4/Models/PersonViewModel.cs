using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignmnet4.Models
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        
        [Required]
        [RegularExpression(@"/^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Enter Valid First Name")]
        public string FirstName { get; set; }
        [RegularExpression(@"/^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Enter Valid Middle Name")]
        public string MiddleName { get; set; }
        [RegularExpression(@"/^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Enter Valid Last Name")]
        [Required]
        public string LastName { get; set; }
        [RegularExpression(@"/^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Enter Valid Email Address")]
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"/^[0-9]$",ErrorMessage ="Enter Valid Number")]
        public string Number { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImagePath { get; set; }
        public string type { get; set; }
        


    }
}