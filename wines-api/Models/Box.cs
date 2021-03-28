using System;
using System.Collections.Generic;

#nullable disable

namespace wines_api.Models
{
    public partial class Box
    {
        public Box()
        {
            Locations = new HashSet<Location>();
        }

        public int Boxno { get; set; }
        public string Size { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
