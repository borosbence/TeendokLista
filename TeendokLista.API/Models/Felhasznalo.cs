using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string felhasznalonev { get; set; } = null!;
        [StringLength(255)]
        public string jelszo { get; set; } = null!;

        [InverseProperty("felhasznalo")]
        public virtual ICollection<Feladat> feladatok { get; set; }
    }
}
