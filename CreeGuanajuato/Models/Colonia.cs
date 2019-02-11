using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CreeGuanajuato.Models
{
    public class Colonia
    {
        [Key]
        public int id_colonia { get; set; }

        [Required]
        [DisplayName("Municipio")]
        public int? id_municipio { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string nombre_colonia { get; set; }

        [DisplayName("CP")]
        [DataType(DataType.PostalCode)]
        public string codigo_postal { get; set; }

        public virtual Municipio Municipio { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string cve_loc { get; set; }
    }
}
