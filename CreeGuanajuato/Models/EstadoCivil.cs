using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models
{
    public class EstadoCivil
    {
        [Key]
        public int id_estado_civil { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
    }
}
