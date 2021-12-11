using System.ComponentModel.DataAnnotations;

namespace StorePractice.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Id { get; set; }
    }
}
