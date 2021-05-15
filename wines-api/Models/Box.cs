using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WinesApi.Models
{
    public partial class Box
    {
        public Box()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        public int Boxno { get; set; }

        public string Size { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}