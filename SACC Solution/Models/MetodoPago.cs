using System.ComponentModel.DataAnnotations;


namespace SACC_Solution.Models
{
    public class MetodoPago
    {
        [Key]
        public int  Id { get; set; }
        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }
    }
}
