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
                Felhasznalonev = source.felhasznalonev,
                Szerepkor = source.szerepkor!.nev
            };
        }

        public static List<FelhasznaloDTO> ToDTO(this IEnumerable<Felhasznalo> source)
        {
            return source.Select(src => src.ToDTO()).ToList();
        }
    }
}