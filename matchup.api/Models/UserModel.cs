using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace matchup.api.Models
{
    public class UserRegistrationModel : IModelBase
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

    public class UserDetailModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        public int Age {get; set;}
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoModel> Photos { get; set; }
    }

    public class UserListModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        public int Age {get; set;}
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}