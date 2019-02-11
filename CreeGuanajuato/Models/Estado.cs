using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models
{
    public class Estado
    {
        [Key]
        public int id_estado { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string nombre_estado { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string cve_agee { get; set; }
    }
}
