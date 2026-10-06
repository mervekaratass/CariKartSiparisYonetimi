namespace CariKartSiparisYonetimi.Entities
{
    public class Siparis
    {
        public int Id { get; set; }
        public int CariKartId { get; set; }
        public string SiparisNo { get; set; } = string.Empty;
        public DateTime SiparisTarihi { get; set; } = DateTime.Now; //başlangıç değeri yok bugünü göstersin
        public string? Aciklama { get; set; }
        public decimal ToplamTutar => Detaylar.Sum(d => d.Tutar);
        public CariKart? CariKart { get; set; }
        public List<SiparisDetayi> Detaylar { get; set; } = new();
  
    }
}
