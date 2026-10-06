using System.ComponentModel.DataAnnotations;

namespace CariKartSiparisYonetimi.Entities
{
    public class Siparis
    {
        public int Id { get; set; }

        [Display(Name = "Cari")]
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir cari seçiniz.")]
        public int CariKartId { get; set; }

        [Display(Name = "Sipariş No")]
        public string SiparisNo { get; set; } = string.Empty;

        [Display(Name = "Sipariş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime SiparisTarihi { get; set; } = DateTime.Now; //başlangıç değeri yok bugünü göstersin


        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }
        public decimal ToplamTutar => Detaylar.Sum(d => d.Tutar);
        public CariKart? CariKart { get; set; }
        public List<SiparisDetayi> Detaylar { get; set; } = new();
  
    }
}
