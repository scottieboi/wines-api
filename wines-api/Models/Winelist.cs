﻿using System;
using System.Collections.Generic;

#nullable disable

namespace wines_api.Models
{
    public partial class Winelist
    {
        public Winelist()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public short? Vintage { get; set; }
        public string Winename { get; set; }
        public int? Winetypeid { get; set; }
        public float? Percentalcohol { get; set; }
        public decimal? Pricepaid { get; set; }
        public short? Yearbought { get; set; }
        public short? Bottlesize { get; set; }
        public string Notes { get; set; }
        public short? Rating { get; set; }
        public short? Drinkrangefrom { get; set; }
        public short? Drinkrangeto { get; set; }
        public int? Regionid { get; set; }
        public int? Vineyardid { get; set; }

        public virtual Region Region { get; set; }
        public virtual Vineyard Vineyard { get; set; }
        public virtual Winetype Winetype { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
