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
    public partial class Felhasznalo
    {
        public Felhasznalo()
        {
            feladatok = new HashSet<Feladat>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(50)]
        public string felhasznalonev { get; set; } = null!;
        [JsonIgnore]
        [StringLength(255)]
        public string jelszo { get; set; } = null!;
        [JsonIgnore]
        [StringLength(50)]
        public string? token { get; set; }
        [JsonIgnore]
        [Column(TypeName = "datetime")]
        public DateTime? token_lejarat { get; set; }

        [JsonIgnore]
        [InverseProperty("felhasznalo")]
        public virtual ICollection<Feladat> feladatok { get; set; }
    }
}
