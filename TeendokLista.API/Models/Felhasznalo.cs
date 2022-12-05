using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TeendokLista.API.Models
{
    [Table("felhasznalok")]
    [Index("felhasznalonev", Name = "felhasznalonev", IsUnique = true)]
    [Index("szerepkor_id", Name = "szerepkor_id")]
    public partial class Felhasznalo
    {
        public Felhasznalo()
        {
            feladatok = new HashSet<Feladat>();
            tokenek = new HashSet<LoginToken>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(50)]
        public string felhasznalonev { get; set; } = null!;
        [StringLength(255)]
        public string jelszo { get; set; } = null!;
        [Column(TypeName = "int(11)")]
        public int szerepkor_id { get; set; }


        [ForeignKey("szerepkor_id")]
        [InverseProperty("felhasznalok")]
        [JsonIgnore]
        // public virtual Szerepkor szerepkor { get; set; } = null!;
        public virtual Szerepkor? szerepkor { get; set; } = null!;

        [InverseProperty("felhasznalo")]
        public virtual ICollection<Feladat> feladatok { get; set; }
        [InverseProperty("felhasznalo")]
        public virtual ICollection<LoginToken> tokenek { get; set; }
    }
}
