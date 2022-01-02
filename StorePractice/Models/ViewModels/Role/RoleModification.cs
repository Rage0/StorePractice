using System.ComponentModel.DataAnnotations;

namespace StorePractice.Models.ViewModels
{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] ToAdd { get; set; }
        public string[] ToDelete { get; set; }
    }
}
