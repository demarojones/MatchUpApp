using System.ComponentModel.DataAnnotations;

namespace matchup.api.Models
{
    public class UserModel : IModelBase
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "You must specify password between 1 and 50 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }

    public class UserLoginModel : IModelBase
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "You must specify password between 1 and 50 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}