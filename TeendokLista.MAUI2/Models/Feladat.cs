using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace TeendokLista.MAUI.Models
{
    public class Feladat : ObservableObject
    {
        public Feladat(int felhasznaloId)
        {
            FelhasznaloId = felhasznaloId;
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string cim = string.Empty;
        public string Cim
        {
            get { return cim; }
            set { SetProperty(ref cim, value); }
        }

        private string? tartalom;
        public string? Tartalom
        {
            get { return tartalom; }
            set { SetProperty(ref tartalom, value); }
        }

        private DateTime hatarido = DateTime.Now;
        public DateTime Hatarido
        {
            get { return hatarido; }
            set { SetProperty(ref hatarido, value); }
        }

        private bool teljesitve;
        public bool Teljesitve
        {
            get { return teljesitve; }
            set { SetProperty(ref teljesitve, value); }
        }

        [JsonPropertyName("felhasznalo_id")]
        public int FelhasznaloId { get; set; }
    }
}
