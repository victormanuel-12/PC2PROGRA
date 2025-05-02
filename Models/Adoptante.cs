using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PC2proyecto.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC2proyecto.Models
{
  public class Adoptante
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public ICollection<Adopcion>? Adoptions
    {
      get; set;
    }
  }
}