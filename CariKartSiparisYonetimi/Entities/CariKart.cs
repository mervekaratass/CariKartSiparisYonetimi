namespace CariKartSiparisYonetimi.Entities
{
    public class CariKart
    {
        public int Id { get; set; }
        public string CariKod { get; set; } = string.Empty; //uyarı vermemesi adına
        public string FirmaAdi { get; set; }= string.Empty;
        public string? VergiNo { get; set; }
        public string? Telefon { get; set; }
        public string? EPosta { get; set; }
        public string? Adres { get; set; }
        public string? LogoYolu { get; set; }
        public bool Durum { get; set; } = true;

        public List<Siparis> Siparisler { get; set; } = new();
    }
}
