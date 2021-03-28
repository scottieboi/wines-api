using System;
using System.Collections.Generic;

#nullable disable

namespace wines_api.Models
{
    public partial class Vineyard
    {
        public Vineyard()
        {
            Winelists = new HashSet<Winelist>();
        }

        public string Vineyard1 { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Winelist> Winelists { get; set; }
    }
}
