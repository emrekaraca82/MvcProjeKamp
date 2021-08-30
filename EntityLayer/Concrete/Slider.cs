using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [StringLength(100)]
        public string SliderName { get; set; }

        [StringLength(300)]
        public string SliderPath { get; set; }
    }
}
