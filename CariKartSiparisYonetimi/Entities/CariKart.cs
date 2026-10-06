using System.ComponentModel.DataAnnotations;

namespace CariKartSiparisYonetimi.Entities
{
    public class CariKart
    {
        public int Id { get; set; }

        [Display(Name = "Cari Kodu")]
        [Required(ErrorMessage = "Cari Kodu boş bırakılamaz.")]
        public string CariKod { get; set; } = string.Empty; //uyarı vermemesi adına
       

        [Display(Name = "Firma Adı")]
        [Required(ErrorMessage = "Firma Adı boş bırakılamaz.")]
        public string FirmaAdi { get; set; }= string.Empty;


        [Display(Name = "Vergi Numarası")]
        public string? VergiNo { get; set; }

      
        [StringLength(20)]
        public string? Telefon { get; set; }

        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? EPosta { get; set; }

        public string? Adres { get; set; }
        public string? LogoYolu { get; set; }
        public bool Durum { get; set; } = true;

        public List<Siparis> Siparisler { get; set; } = new();
    }
}
