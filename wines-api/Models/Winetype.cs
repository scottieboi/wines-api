using System;
using System.Collections.Generic;

#nullable disable

namespace WinesApi.Models
{
    public partial class Winetype
    {
        public Winetype()
        {
            Winelists = new HashSet<Winelist>();
        }

        public string Winetype1 { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Winelist> Winelists { get; set; }
    }
}
