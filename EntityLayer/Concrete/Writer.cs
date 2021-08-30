using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Writer
    {
        [Key]
        public int WriterID { get; set; }

        [StringLength(50)]
        public string WriterName { get; set; }

        [StringLength(50)]
        public string WriterSurName { get; set; }

        [StringLength(250)]
        public string WriterImage { get; set; }

        [StringLength(50)]
        public string WriterTitle { get; set; }

        [StringLength(150)]
        public string WriterAbout { get; set; }

        [StringLength(50)]
        public string WriterEmail { get; set; }

        [StringLength(200)]
        public string WriterPassword { get; set; }
        public bool WriterStatus { get; set; }
        public ICollection<Heading> Headings { get; set; } //Burada Headings Tablosu ile ilişki kuruyor
        public ICollection<Content> Contents { get; set; } //Burada Contents Tablosu ile ilişki kuruyor

    }
}
