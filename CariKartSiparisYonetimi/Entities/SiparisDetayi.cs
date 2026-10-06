using System.ComponentModel.DataAnnotations;

namespace CariKartSiparisYonetimi.Entities
{
    public class SiparisDetayi
    {
        public int Id  { get; set; }
        public int SiparisId { get; set; }

        [Required(ErrorMessage = "Ürün Adı boş bırakılamaz.")]
        public string UrunAdi { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar sıfırdan büyük olmalıdır.")]
        public decimal Miktar { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Birim fiyat negatif olamaz.")]
        public decimal BirimFiyat { get; set; }
        public decimal Tutar {  get; set; }
        public Siparis? Siparis { get; set; }
             


    }
}
