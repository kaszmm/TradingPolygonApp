using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradingPolygon.Models
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Plz Enter A strong Password")]
        public string Password { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PersonImage { get; set; }
    }
}
