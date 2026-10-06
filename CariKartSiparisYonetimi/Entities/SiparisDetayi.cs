namespace CariKartSiparisYonetimi.Entities
{
    public class SiparisDetayi
    {
        public int Id  { get; set; }
        public int SiparisId { get; set; }
        public string UrunAdi { get; set; } = string.Empty;
        public decimal Miktar { get; set; }

        public decimal BirimFiyat { get; set; }
        public decimal Tutar {  get; set; }
        public Siparis? Siparis { get; set; }
             


    }
}
