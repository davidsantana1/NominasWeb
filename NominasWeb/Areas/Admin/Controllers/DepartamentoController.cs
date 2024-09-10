using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nominas.DataAccess.Repository.IRepository;
using Nominas.Models;

namespace NominasWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DepartamentoController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public DepartamentoController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			List<Departamento> objDepartamentoList = _unitOfWork.Departamento.GetAll().Include(e => e.Empleados).ToList();
			return View(objDepartamentoList);
		}

		public IActionResult Crear()
		{
			var empleados = _unitOfWork.Empleado.GetAll();

			ViewBag.empleados = new SelectList(empleados, "Id", "Nombre");

			return View();
		}

		[HttpPost]
		public IActionResult Crear(Departamento obj)
		{
			obj.ResponsableDeArea = _unitOfWork.Empleado.Get(u => u.Id == obj.ResponsableDeAreaId);

			if (ModelState.IsValid)
			{
				_unitOfWork.Departamento.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Departamento creado correctamente.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Editar(int? id)
		{
			var responsablesDeArea = _unitOfWork.Empleado.GetAll().ToList();

			ViewBag.empleados = new SelectList(responsablesDeArea, "Id", "Nombre");

			if (id == null || id == 0)
			{
				return NotFound();
			}
			Departamento? DepartamentoFromDb = _unitOfWork.Departamento.Get(u => u.Id == id);
			if (DepartamentoFromDb == null)
			{
				return NotFound();
			}
			return View(DepartamentoFromDb);
		}

		[HttpPost]
		public IActionResult Editar(Departamento obj)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Departamento.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Departamento actualizado correctamente.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Eliminar(int? id)
		{
			var responsablesDeArea = _unitOfWork.Empleado.GetAll().ToList();

			ViewBag.empleados = new SelectList(responsablesDeArea, "Id", "Nombre");

			if (id == null || id == 0)
			{
				return NotFound();
			}
			Departamento? DepartamentoFromDb = _unitOfWork.Departamento.Get(u => u.Id == id);
			if (DepartamentoFromDb == null)
			{
				return NotFound();
			}
			return View(DepartamentoFromDb);
		}

		[HttpPost, ActionName("Eliminar")]
		public IActionResult EliminarPOST(int? id)
		{
			Departamento obj = _unitOfWork.Departamento.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitOfWork.Departamento.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Departamento eliminado correctamente.";
			return RedirectToAction("Index");
		}
	}
}

