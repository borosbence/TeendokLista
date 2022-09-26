using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TeendokLista.API.Models
{
    [Table("tokenek")]
    [Index("felhasznalo_id", Name = "felhasznalo_id")]
    public partial class Token
    {
        public Token(string token, int felhasznalo_id)
        {
            this.token = token;
            lejarat_datum = DateTime.Now.AddDays(7);
            this.felhasznalo_id = felhasznalo_id;
        }

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
