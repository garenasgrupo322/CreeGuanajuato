using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models
{
    public class Necesidad
    {
        [Key]
        public int id_necesidad { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
    }
}
