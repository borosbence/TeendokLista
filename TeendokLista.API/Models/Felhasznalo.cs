using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

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
            tokenek = new HashSet<Token>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(50)]
        public string felhasznalonev { get; set; } = null!;
        [JsonIgnore]
        [StringLength(255)]
        public string jelszo { get; set; } = null!;
        [Column(TypeName = "int(11)")]
        public int szerepkor_id { get; set; }

        [JsonIgnore]
        [ForeignKey("szerepkor_id")]
        [InverseProperty("felhasznalok")]
        public virtual Szerepkor szerepkor { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("felhasznalo")]
        public virtual ICollection<Feladat> feladatok { get; set; }

        [JsonIgnore]
        [InverseProperty("felhasznalo")]
        public virtual ICollection<Token> tokenek { get; set; }
    }
}
