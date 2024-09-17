using Microsoft.AspNetCore.Mvc;
using Nominas.DataAccess.Repository;

namespace NominasWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReporteNominaController : Controller
    {
        private readonly NominaService _nominaService;

        public ReporteNominaController(NominaService nominaService)
        {
            _nominaService = nominaService;
        }

        public IActionResult Index()
        {
            try
            {
                var reporteNomina = _nominaService.CalcularNominaTotal();
                return View(reporteNomina);
            }
            catch (Exception ex)
            {
                // Manejar la excepción de manera adecuada
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}
