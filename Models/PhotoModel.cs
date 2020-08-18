using ModellenBureau.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Models
{
    //Todo Note. Add image uploading.
    //Add sizes body.
    public class PhotoModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public ContactStatus Status { get; set; }

        public AppUser User { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
