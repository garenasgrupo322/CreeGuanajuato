using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CreeGuanajuato.Models
{
    public class Seccion
    {
        [Key]
        public int id_seccion { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
    }
}
