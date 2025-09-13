using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SACC_Solution.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int  id { get; set; }
        [Required]
        [StringLength(250)]
        public required string nombre { get; set; }
        [Required]
        [StringLength(250)]

        public required string correo_electronico { get; set; }
        [Required]
        [StringLength(250)]

        public required string contrasena { get; set; }
        [Required]
        [StringLength(250)]

        public required string telefono { get; set; }
        [Required]
        [StringLength(250)]

        public required string direccion { get; set; }
        [Required]
        [StringLength(250)]

        public required string especialidad { get; set; }
        [Required]
        [StringLength(250)]

        [Column("estado")]
        public required int estado_usuario { get; set; }

        public required string permisos { get; set; }
        [Required]
        [StringLength(250)]

        public string genero { get; set; }
        [StringLength(250)]

        public required int id_rol { get; set; }
        [Required]

        public DateTime Created_at { get; set; }

    }
}
