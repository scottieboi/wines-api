using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual ICollection<Winelist> Winelists { get; set; }
    }
}