using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WinesApi.Models
{
    public partial class Region
    {
        public Region()
        {
            Winelists = new HashSet<Winelist>();
        }

        [Key]
        public int Id { get; set; }

        public string Region1 { get; set; }

        public virtual ICollection<Winelist> Winelists { get; set; }
    }
}