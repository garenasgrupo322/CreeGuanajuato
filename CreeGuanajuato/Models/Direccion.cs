using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models
{
    public class Direccion
    {
        [Key]
        public int id_direccion { get; set; }

        [Required]
        [DisplayName("Colonia")]
        public int? id_colonia { get; set; }

        [DisplayName("Calle")]
        public string calle { get; set; }
        [DisplayName("Número")]
        public string numero { get; set; }

        public virtual Colonia Colonia { get; set; }
    }
}
