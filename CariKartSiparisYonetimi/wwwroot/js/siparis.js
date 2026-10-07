// Yeni Sipariş ekranı: satır ekleme/silme ve tutarların anlık hesaplanması.
(function () {
    const satirlar = document.getElementById('satirlar');
    const sablon = document.getElementById('satirSablonu');
    const genelToplam = document.getElementById('genelToplam');

    function paraYaz(deger) {
        return deger.toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + ' TL';
    }

    // MVC'nin satırları List<SiparisDetayi> olarak bağlayabilmesi için alan adları
    // Detaylar[0].UrunAdi, Detaylar[1].UrunAdi ... şeklinde boşluksuz sıralı olmalıdır.
    function yenidenNumarala() {
        satirlar.querySelectorAll('tr').forEach(function (satir, sira) {
            satir.querySelectorAll('[data-alan]').forEach(function (alan) {
                alan.name = 'Detaylar[' + sira + '].' + alan.dataset.alan;
            });
        });
    }

    function toplamlariHesapla() {
        let toplam = 0;
        satirlar.querySelectorAll('tr').forEach(function (satir) {
            const miktar = parseFloat(satir.querySelector('[data-alan="Miktar"]').value) || 0;
            const birimFiyat = parseFloat(satir.querySelector('[data-alan="BirimFiyat"]').value) || 0;
            const tutar = miktar * birimFiyat;

            satir.querySelector('.satir-tutar').textContent = paraYaz(tutar);
            toplam += tutar;
        });
        genelToplam.textContent = paraYaz(toplam);
    }

    document.getElementById('satirEkle').addEventListener('click', function () {
        satirlar.appendChild(sablon.content.cloneNode(true));
        yenidenNumarala();
        toplamlariHesapla();
    });

    satirlar.addEventListener('click', function (olay) {
        if (olay.target.classList.contains('satir-sil')) {
            olay.target.closest('tr').remove();
            yenidenNumarala();
            toplamlariHesapla();
        }
    });

    satirlar.addEventListener('input', toplamlariHesapla);

    yenidenNumarala();
    toplamlariHesapla();
})();