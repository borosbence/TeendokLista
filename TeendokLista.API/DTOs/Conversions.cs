using TeendokLista.API.Models;

namespace TeendokLista.API.DTOs
{
    public static class Conversions
    {
        public static FelhasznaloDTO ToDTO(this Felhasznalo source)
        {
            return new FelhasznaloDTO
            {
                Id = source.id,
                FelhasznaloNev = source.felhasznalonev,
                Szerepkor = source.szerepkor.nev
            };
        }

        public static List<FelhasznaloDTO> ToDTO(this IEnumerable<Felhasznalo> source)
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
