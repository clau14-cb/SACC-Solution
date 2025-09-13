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
            {
                Ventas = await context.ventas.ToListAsync();
            }
        }

    }
}
