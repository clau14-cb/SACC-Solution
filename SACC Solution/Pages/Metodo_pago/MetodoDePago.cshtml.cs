using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SACC_Solution.Pages.Metodo_pago
{
    public class MetodoDePagoModel : PageModel
    {
        [BindProperty]
        public required MetodoPago MetodoPago { get; set; }

        public void OnGet()
        {
            MetodoPago = new MetodoPago { Nombre = string.Empty };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Lógica para guardar MetodoPago
            return RedirectToPage("./MetodoDePago");
        }
    }

    public class MetodoPago
    {
        public required string Nombre { get; set; }
    }
}
