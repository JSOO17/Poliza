using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Poliza.DataAccess.Models
{
    public partial class Policy
    {
        public int Id { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateExpired { get; set; }
        public string Placa { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
