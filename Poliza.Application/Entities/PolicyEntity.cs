using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Poliza.Application.Entities
{
    public class PolicyEntity
    {
        public int Id { get; set; }
        [Required]
        public DateTime? DateInit { get; set; }
        [Required]
        public DateTime? DateEnd { get; set; }
        [Required]
        public DateTime? DateExpired { get; set; }
        [StringLength(7)]
        public string Placa { get; set; }
        public int CityId { get; set; }
    }
}
