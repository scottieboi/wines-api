using System;
using System.Collections.Generic;

#nullable disable

namespace WinesApi.Models
{
    public partial class Location
    {
        public int? Wineid { get; set; }
        public int? No { get; set; }
        public int? Box { get; set; }
        public int Id { get; set; }
        public short? Cellarversion { get; set; }

        public virtual Box BoxNavigation { get; set; }
        public virtual Winelist Wine { get; set; }
    }
}
