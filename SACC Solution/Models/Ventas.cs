using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACC_Solution.Models
{
    public class Ventas
    {
        [Key]
        public int Id { get; set; }
        public required DateOnly Fecha { get; set; } 
        public required TimeSpan Hora { get; set; }
        public required decimal Precio { get; set; }
        public required decimal Subtotal { get; set; }
        public required decimal Iva { get; set; }
        public decimal Descuento { get; set; }
        public required decimal Total { get; set; }
        public int MetodoPago { get; set; }
        [ForeignKey("MetodoPago")]
        public MetodoPago? MetodoPagoNavigation { get; set; } // Quita 'required' y hazlo nullable

        [Column("Estado")]
        public int Estado { get; set; }

        public int Id_usuario { get; set; }
        [ForeignKey("Id_usuario")]
        public Usuario? UsuarioNavigation { get; set; } // Quita 'required' y hazlo nullable
        public required int Id_cita { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}