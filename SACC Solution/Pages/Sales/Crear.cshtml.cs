using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SACC_Solution.Models;

namespace SACC_Solution.Pages.Sales
{
    public class CrearModel : PageModel
    {
        private readonly SACC_Solution.Models.AppDbContext _context;

        public CrearModel(SACC_Solution.Models.AppDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> MetodosPago { get; set; } = new();
        public List<SelectListItem> Usuarios { get; set; } = new();

        public IActionResult OnGet()
        {
            MetodosPago = _context.MetodoPago
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                .ToList();

            Usuarios = _context.Usuarios
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.nombre })
                .ToList();

            return Page();
        }

        [BindProperty]
        public Ventas Ventas { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            MetodosPago = _context.MetodoPago
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                .ToList();

            Usuarios = _context.Usuarios
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.nombre })
                .ToList();

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState.IsValid: false");
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Campo: {entry.Key} - Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            _context.ventas.Add(Ventas);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
