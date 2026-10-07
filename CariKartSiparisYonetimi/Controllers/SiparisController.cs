using CariKartSiparisYonetimi.Context;
using CariKartSiparisYonetimi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CariKartSiparisYonetimi.Controllers
{
    public class SiparisController : Controller
    {
        private readonly CariKartSiparisYonetimiDbContext _context;

        public SiparisController(CariKartSiparisYonetimiDbContext context)
        {
            _context = context;
        }

        // SIP-00001, SIP-00002 ... biçiminde sıradaki numarayı üretir.
        private async Task<string> SiradakiSiparisNo()
        {
            var sonId = await _context.Siparisler.MaxAsync(s => (int?)s.Id) ?? 0;
            return $"SIP-{sonId + 1:D5}";
        }

        // Cari seçim listesini hazırlar; sadece aktif cariler seçilebilir.
        private async Task CarileriDoldur()
        {
            var cariler = await _context.CariKartlar
                .Where(c => c.Durum)
                .OrderBy(c => c.FirmaAdi)
                .Select(c => new { c.Id, Ad = c.CariKod + " - " + c.FirmaAdi })
                .ToListAsync();

            ViewBag.Cariler = new SelectList(cariler, "Id", "Ad");
        }

        // Boş sipariş formunu gösterir.
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var siparis = new Siparis
            {
                SiparisNo = await SiradakiSiparisNo()
            };
            siparis.Detaylar.Add(new SiparisDetayi { Miktar = 1 });

            await CarileriDoldur();
            return View(siparis);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Siparis siparis)
        {
            siparis.SiparisNo=await SiradakiSiparisNo();

            if (siparis.Detaylar.Count == 0)
                ModelState.AddModelError(string.Empty, "Siparişe en az 1 satır eklenmelidir.");


                // Seçilen cari gerçekten var mı ve aktif mi
                var cariGecerli = await _context.CariKartlar.AnyAsync(c => c.Id == siparis.CariKartId && c.Durum);
            if (siparis.CariKartId > 0 && !cariGecerli)
                ModelState.AddModelError(nameof(Siparis.CariKartId), "Seçilen cari bulunamadı veya pasif durumda.");

            if (!ModelState.IsValid)
            {
                await CarileriDoldur();
                return View(siparis);
            }

            // Tutar formdan alınmaz; her satır için sunucuda hesaplanır.
            foreach (var detay in siparis.Detaylar)
                detay.Tutar = Math.Round(detay.Miktar * detay.BirimFiyat, 2);

            _context.Siparisler.Add(siparis);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = siparis.Id });


        }


        // Bir siparişi carisi ve satırlarıyla birlikte gösterir.
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var siparis = await _context.Siparisler
                .Include(s => s.CariKart)
                .Include(s => s.Detaylar)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (siparis == null)
                return NotFound();

            return View(siparis);
        }


        // Siparişleri en yeniden eskiye listeler.
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var siparisler = await _context.Siparisler
                .Include(s => s.CariKart)
                .Include(s => s.Detaylar)
                .OrderByDescending(s => s.SiparisTarihi)
                .ThenByDescending(s => s.Id)
                .ToListAsync();

            return View(siparisler);
        }
    }
}
