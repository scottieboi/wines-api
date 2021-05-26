using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Region1 { get; set; }

        public virtual ICollection<Winelist> Winelists { get; set; }
    }
}