using System;
using System.ComponentModel.DataAnnotations;

namespace Poliza.Models
{
    public class PolicyModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime? DateInit { get; set; }
        [Required]
        public DateTime? DateEnd { get; set; }
        [Required]
        public DateTime? DateExpired { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}
