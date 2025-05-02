using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PC2proyecto.Data;
using PC2proyecto.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PC2PROGRA.Controllers
{

  public class AdopcionController : Controller
  {
    private readonly ApplicationDbContext _context;

    public AdopcionController(ApplicationDbContext context)
    {
      _context = context;
    }
    // GET: Adopcion/Index
    public async Task<IActionResult> Adopciones()
    {
      var adopciones = await _context.Adoptions
          .Include(a => a.Pet)
          .Include(a => a.Adopter)
          .ToListAsync();
      return View(adopciones);
    }
    // GET: Adopcion/Create
    public IActionResult Create()
    {
      // Obtener solo mascotas disponibles para adopción
      var pets = _context.Pets
          .Where(p => p.EstadoAdopcion == "Disponible")
          .Select(p => new
          {
            Id = p.Id,
            DisplayName = $"{p.Nombre} ({p.Tipo}) - Edad: {p.Edad}"
          }).ToList();

      ViewBag.PetsList = pets;
      ViewBag.Adopters = new SelectList(_context.Adopters, "Id", "Nombre");
      return View();
    }

    // POST: Adopcion/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PetId,AdopterId,FechaAdopcion")] Adopcion adopcion)
    {


      var mascota = await _context.Pets.FindAsync(adopcion.PetId);

      if (mascota == null)
      {
        TempData["ErrorMessage"] = "La mascota seleccionada no existe.";
        return RedirectToAction(nameof(Create));
      }

      if (mascota.EstadoAdopcion != "Disponible")
      {
        TempData["WarningMessage"] = "Esta mascota ya ha sido adoptada.";
        return RedirectToAction(nameof(Create));
      }


      bool adopcionExists = _context.Adoptions
          .Any(a => a.PetId == adopcion.PetId);

      if (adopcionExists)
      {
        TempData["WarningMessage"] = "Esta mascota ya ha sido asignada a un adoptante.";
      }
      else
      {

        mascota.EstadoAdopcion = "Adoptada";
        _context.Update(mascota);
        adopcion.FechaAdopcion = DateTime.SpecifyKind(adopcion.FechaAdopcion, DateTimeKind.Utc);

        // Guardar la adopción
        _context.Add(adopcion);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "¡Adopción registrada correctamente!";
      }



      return RedirectToAction(nameof(Create));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }
  }
}