using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD_App.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        [Required]
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
