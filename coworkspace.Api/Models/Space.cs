using System.ComponentModel.DataAnnotations;

namespace Coworkspace.Api.Models
{
    public class Space
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public bool IsAvailable { get; set; }
    }
}