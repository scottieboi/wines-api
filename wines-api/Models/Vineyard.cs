using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WinesApi.Models
{
    public partial class Vineyard
    {
        public Vineyard()
        {
            Winelists = new HashSet<Winelist>();
        }

        public string Vineyard1 { get; set; }

        [Key]
        public int Id { get; set; }

        public virtual ICollection<Winelist> Winelists { get; set; }
    }
}