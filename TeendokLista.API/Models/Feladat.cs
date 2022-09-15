using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TeendokLista.API.Models
{
    [Table("feladatok")]
    [Index("felhasznalo_id", Name = "felhasznalo_id")]
    public partial class Feladat
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(50)]
        public string cim { get; set; } = null!;
        [Column(TypeName = "text")]
        public string? tartalom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime hatarido { get; set; }
        public bool teljesitve { get; set; }
        [Column(TypeName = "int(11)")]
        public int felhasznalo_id { get; set; }

        [ForeignKey("felhasznalo_id")]
        [InverseProperty("feladatok")]
        // public virtual Felhasznalo felhasznalo { get; set; } = null!;
        public virtual Felhasznalo? felhasznalo { get; set; }
    }
}
