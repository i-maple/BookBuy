using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CRUDApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        //[Key]
        //public string UserId { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? StreetAddress { get; set; }
        [ForeignKey("CompanyId")]
        public int? CompanyId { get; set; }
        [ValidateNever]
        public Company Company { get; set; }
    }
}
