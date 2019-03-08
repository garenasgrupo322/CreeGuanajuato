using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CreeGuanajuato.Models
{
    public class Legal
    {
        [Key]
        public int id_legal{ get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
    }
}
