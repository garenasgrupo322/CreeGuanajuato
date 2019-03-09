using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models
{
    public class Registro
    {
        [Key]
        public int id_registro { get; set; }

        [DisplayName("Estado")]
        public int? id_estado { get; set; }

        [DisplayName("Municipio")]
        public int? id_municipio { get; set; }

        [DisplayName("Colonia")]
        public int? id_colonia { get; set; }

        [DisplayName("Dirección")]
        public int? id_direccion { get; set; }

        [DisplayName("Escolaridad")]
        public int? id_escolaridad { get; set; }

        [DisplayName("Estado Civil")]
        public int? id_estado_civil { get; set; }

        [DisplayName("Necesidad")]
        public int? id_necesidad { get; set; }

        [DisplayName("Seccional")]
        public int? id_seccion { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Required]
        [DisplayName("Apellido paterno")]
        public string apellido_paterno { get; set; }

        [Required]
        [DisplayName("Apellido materno")]
        public string apellido_materno { get; set; }

        public string NormalizedNombre { get; set; }

        [Required]
        [DisplayName("INE")]
        public string INE { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date)]
        public DateTime fecha_nacimiento { get; set; }

        [Required]
        [Range(1,20, ErrorMessage = "El valor debe de ser entre 1 y 20")]
        [Display(Name = "Número de hijos")]
        public int numero_hijos { get; set; }

        public double longitud { get; set; }

        public double latitud { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Colonia Colonia { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual Escolaridad Escolaridad { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }
        public virtual Necesidad Necesidad { get; set; }
        public virtual Seccion Seccion { get; set; }
    }
}
