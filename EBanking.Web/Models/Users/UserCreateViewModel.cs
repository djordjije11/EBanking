using System.ComponentModel.DataAnnotations;

namespace EBanking.Web.Models.Users
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Morate uneti ime korisnika")]
        public string FirstName { get; set; }
        [MinLength(2)]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Nevalidan format email adrese")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
