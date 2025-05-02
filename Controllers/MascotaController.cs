
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC2proyecto.Models;
using PC2proyecto.Data;

namespace PC2PROGRA.Controllers
{

  public class MascotaController : Controller
  {
    private readonly ApplicationDbContext _context;

    public MascotaController(ApplicationDbContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      return View();
    }


    public IActionResult Create()
    {
      return View();
    }

    // POST: Mascota/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Edad,Tipo,EstadoAdopcion")] Mascota mascota)
    {
      if (ModelState.IsValid)
      {
        _context.Add(mascota);
        await _context.SaveChangesAsync();

        // Agregar mensaje de confirmación
        TempData["SuccessMessage"] = $"¡La mascota {mascota.Nombre} ha sido registrada exitosamente!";

        return RedirectToAction(nameof(Create));
      }
      return View(mascota);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }
  }
}