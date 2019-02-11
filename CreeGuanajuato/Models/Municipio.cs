using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models
{
    public class Municipio
    {
        [Key]
        public int id_municipio { get; set; }

        [Required]
        [DisplayName("Estado")]
        public int? id_estado { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string nombre_municipio { get; set; }

        public virtual Estado Estado { get; set; }

        [ScaffoldColumn(false)]
        public string cve_agem { get; set; }
    }
}
