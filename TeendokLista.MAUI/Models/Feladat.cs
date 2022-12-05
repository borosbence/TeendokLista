using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;
using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Models
{
    public class Feladat : ObservableObject
    {
        public Feladat()
        {
            Hatarido = DateTime.Now;
            FelhasznaloId = CurrentUser.Id;
        }

        private int id;
        [JsonPropertyName("id")]
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string cim;
        [JsonPropertyName("cim")]
        public string Cim
        {
            get { return cim; }
            set { SetProperty(ref cim, value); }
        }

        private string? tartalom;
        [JsonPropertyName("tartalom")]
        public string? Tartalom
        {
            get { return tartalom; }
            set { SetProperty(ref tartalom, value); }
        }

        private DateTime hatarido;
        [JsonPropertyName("hatarido")]
        public DateTime Hatarido
        {
            get { return hatarido; }
            set { SetProperty(ref hatarido, value); }
        }

        private bool teljesitve;
        [JsonPropertyName("teljesitve")]
        public bool Teljesitve
        {
            get { return teljesitve; }
            set { SetProperty(ref teljesitve, value); }
        }

        [JsonPropertyName("felhasznalo_id")]
        public int FelhasznaloId { get; set; }
    }
}
