using CariKartSiparisYonetimi.Context;
using CariKartSiparisYonetimi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CariKartSiparisYonetimi.Controllers
{
    public class CariKartController : Controller
    {
        private static readonly string[] IzinliUzantilar = { ".jpg", ".jpeg", ".png" };
        private const long AzamiLogoBoyutu = 2 * 1024 * 1024; // 2 MB
        private const string LogoKlasoru = "uploads/logolar";
        private readonly IWebHostEnvironment _env;


        private readonly  CariKartSiparisYonetimiDbContext _context;
        public CariKartController(CariKartSiparisYonetimiDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> List()
        {
            var cariler = await _context.CariKartlar.
                OrderBy(c => c.CariKod).ToListAsync();

            return View(cariler);
        }

        // Boş formu gösterir.
        [HttpGet]
        public  IActionResult Create()
        {
            return View(new CariKart());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CariKart cariKart, IFormFile? logoDosyasi)
        {
            //Böyle bir kod varmı
            var kod = await _context.CariKartlar.AnyAsync(c => c.CariKod == cariKart.CariKod);

            if (kod)
                ModelState.AddModelError(nameof(CariKart.CariKod), "Bu cari kodu başka bir cari kartta kullanılıyor.");

            LogoKontrolEt(logoDosyasi);

            if (!ModelState.IsValid)
                return View(cariKart);

            cariKart.LogoYolu = await LogoKaydet(logoDosyasi);

            await _context.CariKartlar.AddAsync(cariKart);
             await _context.SaveChangesAsync();


            return RedirectToAction(nameof(List));
        }



        //Düzenlenicek cariyi forma dolu getir.
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
          var cariKart= await _context.CariKartlar.FindAsync(id);

            if (cariKart == null)
                return NotFound();

            return View(cariKart);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CariKart form, IFormFile? logoDosyasi)
        {
            var cariKart = await _context.CariKartlar.FindAsync(id);

            if (cariKart == null)
                return NotFound();


            var kod = await _context.CariKartlar.AnyAsync(c => c.CariKod == form.CariKod && c.Id!=cariKart.Id);
            if (kod)
                ModelState.AddModelError(nameof(CariKart.CariKod), "Bu cari kodu başka bir cari kartta kullanılıyor.");


            LogoKontrolEt(logoDosyasi);
            form.LogoYolu = cariKart.LogoYolu;   

            if (!ModelState.IsValid)
                return View(form);

            cariKart.CariKod = form.CariKod;
            cariKart.FirmaAdi = form.FirmaAdi;
            cariKart.VergiNo = form.VergiNo;
            cariKart.Telefon = form.Telefon;
            cariKart.EPosta = form.EPosta;
            cariKart.Adres = form.Adres;

            // Yeni logo seçildiyse eskisini sil, yenisini kaydet. Seçilmediyse mevcut logo kalır.
            if (logoDosyasi != null)
            {
                LogoSil(cariKart.LogoYolu);
                cariKart.LogoYolu = await LogoKaydet(logoDosyasi);
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));

        }


        // Cari silinmez; pasife alınır veya tekrar aktif edilir.
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var cariKart = await _context.CariKartlar.FindAsync(id);

            if (cariKart == null)
                return NotFound();

            cariKart.Durum = !cariKart.Durum;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }



        // Dosya JPG/PNG mi ve boyutu uygun mu
        private void LogoKontrolEt(IFormFile? logoDosyasi)
        {
            if (logoDosyasi == null)
                return;

            var uzanti = Path.GetExtension(logoDosyasi.FileName).ToLowerInvariant();

            if (!IzinliUzantilar.Contains(uzanti))
                ModelState.AddModelError(nameof(logoDosyasi), "Logo için sadece JPG veya PNG dosyası yüklenebilir.");
            else if (logoDosyasi.Length > AzamiLogoBoyutu)
                ModelState.AddModelError(nameof(logoDosyasi), "Logo dosyası en fazla 2 MB olabilir.");
        }

        // Dosyayı wwwroot/uploads/logolar altına kaydeder ve yolunu döner
        private async Task<string?> LogoKaydet(IFormFile? logoDosyasi)
        {
            if (logoDosyasi == null)
                return null;

            var klasor = Path.Combine(_env.WebRootPath, LogoKlasoru);
            Directory.CreateDirectory(klasor);

            // Kullanıcının verdiği dosya adı kullanılmaz; yeni ve benzersiz bir ad üretilir.
            var dosyaAdi = Guid.NewGuid().ToString("N") + Path.GetExtension(logoDosyasi.FileName).ToLowerInvariant();

            using (var akis = new FileStream(Path.Combine(klasor, dosyaAdi), FileMode.Create))
            {
                await logoDosyasi.CopyToAsync(akis);
            }

            return $"/{LogoKlasoru}/{dosyaAdi}";
        }

        // Eski logo dosyasını diskten siler
        private void LogoSil(string? logoYolu)
        {
            if (string.IsNullOrEmpty(logoYolu))
                return;

            var tamYol = Path.Combine(_env.WebRootPath, logoYolu.TrimStart('/'));

            if (System.IO.File.Exists(tamYol))
                System.IO.File.Delete(tamYol);
        }
    }
}
