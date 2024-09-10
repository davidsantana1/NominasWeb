using Microsoft.AspNetCore.Mvc;
using Nominas.DataAccess.Data;
using Nominas.Models;

namespace NominasWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PuestoController : Controller
	{
		private readonly ApplicationDbContext _db;
		public PuestoController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			List<Puesto> objPuestoList = _db.Puestos.ToList();
			return View(objPuestoList);
		}

		public IActionResult Crear()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Crear(Puesto obj)
		{
			if (ModelState.IsValid)
			{
				_db.Puestos.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Puesto creado correctamente.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Editar(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Puesto? puestoFromDb = _db.Puestos.Find(id);
			if (puestoFromDb == null)
			{
				TempData["error"] = "El id del puesto es incorrecto.";
				return RedirectToAction("Index");
			}
			return View(puestoFromDb);
		}

		[HttpPost]
		public IActionResult Editar(Puesto obj)
		{
			if (ModelState.IsValid)
			{
				_db.Puestos.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Puesto actualizado correctamente.";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Eliminar(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Puesto? puestoFromDb = _db.Puestos.Find(id);
			if (puestoFromDb == null)
			{
				return NotFound();
			}
			return View(puestoFromDb);
		}

		[HttpPost, ActionName("Eliminar")]
		public IActionResult EliminarPOST(int? id)
		{
			Puesto obj = _db.Puestos.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.Puestos.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Puesto eliminado correctamente.";
			return RedirectToAction("Index");
		}
	}
}

