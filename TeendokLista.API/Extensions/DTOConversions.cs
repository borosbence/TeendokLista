using TeendokLista.API.DTOs;
using TeendokLista.API.Models;

namespace TeendokLista.API.Extensions
{
    public static class DTOConversions
    {
        public static FelhasznaloDTO toDTO(this Felhasznalo source)
        {
            return new FelhasznaloDTO
            {
                Id = source.id,
                FelhasznaloNev = source.felhasznalonev,
                Szerepkor = source.szerepkor.nev
            };
        }

        public static List<FelhasznaloDTO> toDTO(this IEnumerable<Felhasznalo> source)
        {
            return source.Select(src => new FelhasznaloDTO
            {
                Id = src.id,
                FelhasznaloNev = src.felhasznalonev,
                Szerepkor = src.szerepkor.nev
            }).ToList();
        }
    }
}
