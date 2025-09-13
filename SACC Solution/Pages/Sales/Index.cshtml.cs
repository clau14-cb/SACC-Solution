using SACC_Solution.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace SACC_Solution.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext context;
        public IndexModel(AppDbContext context)
        {
            this.context = context;
        }
        public IList<Ventas> Ventas { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Ventas = await context.ventas
                .Include(v => v.MetodoPagoNavigation)
                .Include(v => v.UsuarioNavigation)
                .Select(v => new Ventas {
                    Id = v.Id,
                    Fecha = v.Fecha,
                    Hora = v.Hora,
                    Precio = v.Precio,
                    Subtotal = v.Subtotal,
                    Iva = v.Iva,
                    Descuento = v.Descuento,
                    Total = v.Total,
                    MetodoPago = v.MetodoPago,
                    MetodoPagoNavigation = v.MetodoPagoNavigation,
                    Estado = v.Estado, // aquí se asegura que es el de ventas
                    Id_usuario = v.Id_usuario,
                    UsuarioNavigation = v.UsuarioNavigation,
                    Id_cita = v.Id_cita,
                    CreatedAt = v.CreatedAt
                })
                .ToListAsync();
        }

    }
}
