using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SACC_Solution.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly SACC_Solution.Models.AppDbContext _context;

        public IndexModel(IConfiguration configuration, SACC_Solution.Models.AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        [BindProperty]
        public int? MetodoPagoId { get; set; }
        [BindProperty]
        public int? CreadoPorId { get; set; }

        public List<ResultadoConsulta> Resultados { get; set; } = new();
        public List<SelectListItem> MetodosPago { get; set; } = new();
        public List<SelectListItem> Usuarios { get; set; } = new();

        public void OnGet()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            CargarListas();
        }

        private void CargarListas()
        {
            MetodosPago = _context.MetodoPago
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                .ToList();

            Usuarios = _context.Usuarios
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.nombre })
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CargarListas();

            var resultados = new List<ResultadoConsulta>();
            string connectionString = _configuration.GetConnectionString("ConexionSQL");

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Construir la consulta SQL con filtros condicionales
                var sql = @"SELECT
                        v.Fecha,
                        v.Hora,
                        v.Precio,
                        v.Subtotal,
                        v.Iva,
                        v.Total,
                        mp.nombre as metodo_pago,
                        c.modalidad,
                        c.estado,
                        a.ubicacion,
                        u.nombre,
                        u.especialidad
                    FROM
                        ventas v 
                        inner join MetodoPago mp on v.MetodoPago = mp.id
                        inner join cita c on v.Id_cita = c.id 
                        inner join agenda a on c.id_agenda = a.id 
                        inner join usuario u on c.id_usuario = u.id
                    WHERE DATE(v.CreatedAt) >= DATE(@StartDate) AND DATE(v.CreatedAt) <= DATE(@EndDate)
                ";
                if (MetodoPagoId.HasValue)
                {
                    sql += " AND v.MetodoPago = @MetodoPagoId ";
                }
                if (CreadoPorId.HasValue)
                {
                    sql += " AND v.Id_usuario = @CreadoPorId ";
                }

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    if (MetodoPagoId.HasValue)
                        command.Parameters.AddWithValue("@MetodoPagoId", MetodoPagoId);
                    if (CreadoPorId.HasValue)
                        command.Parameters.AddWithValue("@CreadoPorId", CreadoPorId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resultados.Add(new ResultadoConsulta
                            {
                                Fecha = reader.GetDateTime("Fecha"),
                                Hora = reader.GetTimeSpan("Hora").ToString(),
                                Precio = reader.GetDecimal("Precio"),
                                Subtotal = reader.GetDecimal("Subtotal"),
                                Iva = reader.GetDecimal("Iva"),
                                Total = reader.GetDecimal("Total"),
                                MetodoPago = reader.GetString("metodo_pago"),
                                Modalidad = reader.GetString("modalidad"),
                                Estado = reader.GetString("estado"),
                                Ubicacion = reader.GetString("ubicacion"),
                                NombreUsuario = reader.GetString("nombre"),
                                Especialidad = reader.GetString("especialidad")
                            });
                        }
                    }
                }
            }

            Resultados = resultados;
            return Page();
        }

        public class ResultadoConsulta
        {
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public decimal Precio { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Iva { get; set; }
            public decimal Total { get; set; }
            public string MetodoPago { get; set; }
            public string Modalidad { get; set; }
            public string Estado { get; set; }
            public string Ubicacion { get; set; }
            public string NombreUsuario { get; set; }
            public string Especialidad { get; set; }
        }
    }
}
