using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TeendokLista.API.Models
{
    [Table("szerepkorok")]
    [Index("nev", Name = "nev", IsUnique = true)]
    public partial class Szerepkor
    {
        public Szerepkor()
        {
            felhasznalok = new HashSet<Felhasznalo>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(50)]
        public string nev { get; set; } = null!;

        [InverseProperty("szerepkor")]
        public virtual ICollection<Felhasznalo> felhasznalok { get; set; }
    }
}
