using System.ComponentModel.DataAnnotations;

namespace web_aplikace.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Jméno je povinné")]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Příjmení je povinné")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Heslo je povinné")]
        [MinLength(6, ErrorMessage = "Heslo musí mít alespoň 6 znaků")]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = string.Empty;
    }
}
