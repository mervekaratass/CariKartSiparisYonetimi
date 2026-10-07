using CariKartSiparisYonetimi.Context;
using CariKartSiparisYonetimi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CariKartSiparisYonetimi.Controllers
{
    public class CariKartController : Controller
    {
       private readonly  CariKartSiparisYonetimiDbContext _context;

        public CariKartController(CariKartSiparisYonetimiDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create(CariKart cariKart)
        {
            //Böyle bir kod varmı
            var kod = await _context.CariKartlar.AnyAsync(c => c.CariKod == cariKart.CariKod);

            if (kod)
                ModelState.AddModelError(nameof(CariKart.CariKod), "Bu cari kodu başka bir cari kartta kullanılıyor.");
            

            if (!ModelState.IsValid)
                return View(cariKart);
         

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
        public async Task<IActionResult> Edit(int id, CariKart form)
        {
            var cariKart = await _context.CariKartlar.FindAsync(id);

            if (cariKart == null)
                return NotFound();


            var kod = await _context.CariKartlar.AnyAsync(c => c.CariKod == form.CariKod && c.Id!=cariKart.Id);
            if (kod)
                ModelState.AddModelError(nameof(CariKart.CariKod), "Bu cari kodu başka bir cari kartta kullanılıyor.");

            if (!ModelState.IsValid)
                return View(form);

            cariKart.CariKod = form.CariKod;
            cariKart.FirmaAdi = form.FirmaAdi;
            cariKart.VergiNo = form.VergiNo;
            cariKart.Telefon = form.Telefon;
            cariKart.EPosta = form.EPosta;
            cariKart.Adres = form.Adres;

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
    }
}
