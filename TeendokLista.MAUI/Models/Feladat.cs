using CommunityToolkit.Mvvm.ComponentModel;

namespace TeendokLista.MAUI.Models
{
    public class Feladat /*:ObservableObject*/
    {
        //private string _cim;
        //public string cim
        //{
        //    get { return _cim; }
        //    set { _cim = value; OnPropertyChanged(); }
        //}

        public int id { get; set; }
        // public string cim { get; set; }
        public string tartalom { get; set; }
        public DateTime hatarido { get; set; }
        public bool teljesitve { get; set; }
    }
}
