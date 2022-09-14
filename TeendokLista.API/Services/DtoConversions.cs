using TeendokLista.API.DTOs;
using TeendokLista.API.Models;

namespace TeendokLista.API.Services
{
    public static class DtoConversions
    {
        public static FeladatDto toDto(this Feladat feladat)
        {
            return new FeladatDto
            {
                Id = feladat.id,
                Cim = feladat.cim,
                Tartalom = feladat.tartalom,
                Hatarido = feladat.hatarido,
                Teljesitve = feladat.teljesitve,
                FelhasznaloId = feladat.felhasznalo_id
            };
        }

        public static List<FeladatDto> toDto(this List<Feladat> feladatok)
        {
            return feladatok.Select(feladat => 
            {
                return new FeladatDto
                {
                    Id = feladat.id,
                    Cim = feladat.cim,
                    Tartalom = feladat.tartalom,
                    Hatarido = feladat.hatarido,
                    Teljesitve = feladat.teljesitve,
                    FelhasznaloId = feladat.felhasznalo_id
                };
            }).ToList();
        }
    }
}
