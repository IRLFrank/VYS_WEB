using System.ComponentModel.DataAnnotations;

namespace web_aplikace.Views.User
{
    public class Uzivatel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string prezdivka { get; set; }
        [Required]
        public string heslo { get; set; }
    }
}
