using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nominas.DataAccess.Repository.IRepository;
using Nominas.Models;

namespace NominasWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RegistroTransaccionController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public RegistroTransaccionController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			List<RegistroTransaccion> objRegistroTransaccionList = _unitOfWork.RegistroTransaccion.GetAll().Include(e => e.Empleado).ToList();
			return View(objRegistroTransaccionList);
		}

		public IActionResult Crear()
		{
			var empleados = _unitOfWork.Empleado.GetAll();

			ViewBag.empleados = new SelectList(empleados, "Id", "Nombre");

			return View();
		}

		[HttpPost]
		public IActionResult Crear(RegistroTransaccion obj)
		{
			obj.Empleado = _unitOfWork.Empleado.Get(u => u.Id == obj.EmpleadoId);

			if (ModelState.IsValid)
			{
				_unitOfWork.RegistroTransaccion.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Transacción creada correctamente.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Editar(int? id)
		{
			var empleados = _unitOfWork.Empleado.GetAll().ToList();

			ViewBag.empleados = new SelectList(empleados, "Id", "Nombre");

			if (id == null || id == 0)
			{
				return NotFound();
			}
			RegistroTransaccion? RegistroTransaccionFromDb = _unitOfWork.RegistroTransaccion.Get(u => u.Id == id);
			if (RegistroTransaccionFromDb == null)
			{
				return NotFound();
			}
			return View(RegistroTransaccionFromDb);
		}

		[HttpPost]
		public IActionResult Editar(RegistroTransaccion obj)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.RegistroTransaccion.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Transacción actualizada correctamente.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Eliminar(int? id)
		{
			var empleados = _unitOfWork.Empleado.GetAll().ToList();

			ViewBag.empleados = new SelectList(empleados, "Id", "Nombre");

			if (id == null || id == 0)
			{
				return NotFound();
			}
			RegistroTransaccion? RegistroTransaccionFromDb = _unitOfWork.RegistroTransaccion.Get(u => u.Id == id);
			if (RegistroTransaccionFromDb == null)
			{
				return NotFound();
			}
			return View(RegistroTransaccionFromDb);
		}

		[HttpPost, ActionName("Eliminar")]
		public IActionResult EliminarPOST(int? id)
		{
			RegistroTransaccion obj = _unitOfWork.RegistroTransaccion.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitOfWork.RegistroTransaccion.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Transacción eliminada correctamente.";
			return RedirectToAction("Index");
		}
	}
}
