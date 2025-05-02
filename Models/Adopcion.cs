using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PC2proyecto.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC2proyecto.Models
{
  public class Adopcion
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "La  mascota es obligatoria.")]
    public int PetId { get; set; }

    [Required(ErrorMessage = "El adoptante es obligatoria.")]
    public int AdopterId { get; set; }

    public DateTime FechaAdopcion { get; set; } = DateTime.UtcNow;

    [ForeignKey("PetId")]
    public Mascota Pet { get; set; }

    [ForeignKey("AdopterId")]
    public Adoptante Adopter { get; set; }
  }
}