using System.ComponentModel.DataAnnotations;

namespace web_aplikace.Models
{
    public class Uzivatel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Jmeno { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Prijmeni { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Prezdivka { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Heslo { get; set; } = string.Empty;

        public DateTime DatumRegistrace { get; set; } = DateTime.Now;
    }
}
