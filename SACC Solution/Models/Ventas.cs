using System.ComponentModel.DataAnnotations;

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
        public required string MetodoPago { get; set; }

        public required bool Estado { get; set; } = true;

        public required int Id_usuario { get; set; }
        public required int Id_cita { get; set; }
        public DateTime Created_at { get; set; }
    }
}