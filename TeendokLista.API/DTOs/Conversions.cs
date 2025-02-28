using TeendokLista.API.Models;

namespace TeendokLista.API.DTOs
{
    public static class Conversions
    {
        public static FelhasznaloDTO ToDTO(this Felhasznalo felhasznalo)
        {
            return new FelhasznaloDTO(felhasznalo.id, felhasznalo.felhasznalonev, felhasznalo.szerepkor!.nev);
        }

        public static List<FelhasznaloDTO> ToDTO(this IEnumerable<Felhasznalo> felhasznalo)
        {
            return felhasznalo.Select(ToDTO).ToList();
        }
    }
}