using System;
using System.Collections.Generic;

#nullable disable

namespace wines_api.Models
{
    public partial class Region
    {
        public Region()
        {
            Winelists = new HashSet<Winelist>();
        }

        public int Id { get; set; }
        public string Region1 { get; set; }

        public virtual ICollection<Winelist> Winelists { get; set; }
    }
}
