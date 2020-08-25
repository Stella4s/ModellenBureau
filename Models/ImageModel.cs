using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Models
{
    public abstract class ImageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }

        //In tutorial named UploadedBy.
        public string AppUserId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
