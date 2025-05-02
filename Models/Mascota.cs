using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PC2proyecto.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PC2proyecto.Models
{
  public class Mascota
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    public string Nombre { get; set; }

    [Range(0, 50, ErrorMessage = "La edad debe estar entre 0 y 50 años.")]
    public int Edad { get; set; }

    [Required(ErrorMessage = "El tipo de mascota es obligatorio.")]
    [StringLength(50, ErrorMessage = "El tipo no puede exceder los 50 caracteres.")]
    public string Tipo { get; set; } // perro, gato, etc.

    [Required(ErrorMessage = "El estado de adopción es obligatorio.")]
    [StringLength(20, ErrorMessage = "El estado de adopción no puede exceder los 20 caracteres.")]
    public string EstadoAdopcion { get; set; } = "Disponible"; // Disponible o Adoptada

    public Adopcion? Adoption { get; set; }
  }
}