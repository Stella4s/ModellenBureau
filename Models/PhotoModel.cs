using ModellenBureau.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
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

        public EyeColours EyeColour { get; set; }
        public HairColours HairColour { get; set; }
        public ContactStatus Status { get; set; }


        public AppUser User { get; set; }
        public string AppUserId { get; set; }

        public Collection<ImgOnDatabaseModel> Photos { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }

    public enum EyeColours
    {
        Blue,
        Green,
        Yellow,
        Orange,
        Hazel,
        Odd_Coloured
    }

    public enum HairColours
    {
        Black,
        Brown,
        Orange,
        White,
        Grey,
        Other
    }
}
