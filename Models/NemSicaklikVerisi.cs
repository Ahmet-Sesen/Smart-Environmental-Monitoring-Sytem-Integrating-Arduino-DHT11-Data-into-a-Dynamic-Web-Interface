using System;
using MyWebApi.Models;


namespace MyWebApi.Models
{
    public class NemSicaklikVerisi
    {
        public int Id { get; set; } // Veri kimliği (ID)
        public DateTime Saat { get; set; } // Saat bilgisi
        public double Sicaklik { get; set; } // Sıcaklık değeri
        public double Nem { get; set; } // Nem değeri
    }
}