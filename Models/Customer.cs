using ModellenBureau.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Models
{
    //Todo Note add logo. (Image support.)
    public class Customer
    {
        public int Id { get; set; }

        //8 Cijfers
        public int KvKNummer { get; set; }

        //LandCode NL, 9 Cijfers, Letter B, controlegetal.
        //Example:  NL 123456789B01.
        public string BTWNummer { get; set; }

        public AppUser User { get; set; }
        [ForeignKey("User")]
        public string AppUserId { get; set; }
    }
}
