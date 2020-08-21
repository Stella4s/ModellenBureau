using Microsoft.AspNetCore.Identity;
using ModellenBureau.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Data
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Name")]
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public PhotoModel PhotoModel { get; set; }
        public Customer Customer { get; set; }
    }
}
