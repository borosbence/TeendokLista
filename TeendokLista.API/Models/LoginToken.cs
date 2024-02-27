using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeendokLista.API.Models
{
    [Table("login_tokenek")]
    [Index("felhasznalo_id", Name = "felhasznalo_id")]
    public partial class LoginToken
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(50)]
        public string token { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime lejarat_datum { get; set; }
        [Column(TypeName = "int(11)")]
        public int felhasznalo_id { get; set; }

        [ForeignKey("felhasznalo_id")]
        [InverseProperty("tokenek")]
        public virtual Felhasznalo felhasznalo { get; set; } = null!;
    }
}
