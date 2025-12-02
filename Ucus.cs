using System;
using System.Collections.Generic;

namespace UcakBiletiRezervasyonSistemi
{
    // Uçuş sınıfı
    public class Ucus
    {
        // Private field'lar
        private string _ucusNo;
        private string _kalkisYeri;
        private string _varisYeri;
        private DateTime _kalkisTarihi;
        private TimeSpan _kalkisSaati;
        private Ucak _ucak;
        private decimal _biletFiyati;
        private List<Koltuk> _koltuklar;

        // Public property'ler
        public string UcusNo
        {
            get { return _ucusNo; }
            set { _ucusNo = value; }
        }

        public string KalkisYeri
        {
            get { return _kalkisYeri; }
            set { _kalkisYeri = value; }
        }

        public string VarisYeri
        {
            get { return _varisYeri; }
            set { _varisYeri = value; }
        }

        public DateTime KalkisTarihi
        {
            get { return _kalkisTarihi; }
            set { _kalkisTarihi = value; }
        }

        public TimeSpan KalkisSaati
        {
            get { return _kalkisSaati; }
            set { _kalkisSaati = value; }
        }

        public Ucak Ucak
        {
            get { return _ucak; }
            set { _ucak = value; }
        }

        public decimal BiletFiyati
        {
            get { return _biletFiyati; }
            set { _biletFiyati = value; }
        }

        public List<Koltuk> Koltuklar
        {
            get { return _koltuklar; }
            set { _koltuklar = value; }
        }

        // Boş koltuk sayısı (hesaplanmış property)
        public int BosKoltukSayisi
        {
            get
            {
                int sayi = 0;
                foreach (var koltuk in _koltuklar)
                {
                    if (!koltuk.Dolu)
                        sayi++;
                }
                return sayi;
            }
        }

        // Constructor
        public Ucus(string ucusNo, string kalkisYeri, string varisYeri, DateTime kalkisTarihi,
                    TimeSpan kalkisSaati, Ucak ucak, decimal biletFiyati)
        {
            _ucusNo = ucusNo;
            _kalkisYeri = kalkisYeri;
            _varisYeri = varisYeri;
            _kalkisTarihi = kalkisTarihi;
            _kalkisSaati = kalkisSaati;
            _ucak = ucak;
            _biletFiyati = biletFiyati;
            _koltuklar = new List<Koltuk>();

            // Koltukları otomatik oluştur
            KoltuklariOlustur();
        }

        // Koltukları oluştur (6 koltuk per sıra: A-B-C | D-E-F)
        private void KoltuklariOlustur()
        {
            int siraSayisi = _ucak.Kapasite / 6;
            string[] koltukHarfleri = { "A", "B", "C", "D", "E", "F" };
            KoltukTipi[] tipler = { KoltukTipi.Pencere, KoltukTipi.Orta, KoltukTipi.Koridor,
                                    KoltukTipi.Koridor, KoltukTipi.Orta, KoltukTipi.Pencere };

            for (int sira = 1; sira <= siraSayisi; sira++)
            {
                for (int i = 0; i < 6; i++)
                {
                    string koltukNo = $"{sira}{koltukHarfleri[i]}";
                    _koltuklar.Add(new Koltuk(koltukNo, tipler[i]));
                }
            }
        }

        // Uçuş bilgilerini göster
        public string BilgiGoster()
        {
            return $"{_ucusNo} | {_kalkisYeri} → {_varisYeri} | " +
                   $"{_kalkisTarihi:dd.MM.yyyy} {_kalkisSaati:hh\\:mm} | " +
                   $"Fiyat: {_biletFiyati:C} | Boş Koltuk: {BosKoltukSayisi}";
        }

        // Boş koltukları listele
        public List<Koltuk> BosKoltuklariGetir()
        {
            List<Koltuk> bosKoltuklar = new List<Koltuk>();
            foreach (var koltuk in _koltuklar)
            {
                if (!koltuk.Dolu)
                    bosKoltuklar.Add(koltuk);
            }
            return bosKoltuklar;
        }

        // Koltuk numarasına göre koltuk bul
        public Koltuk KoltukBul(string koltukNo)
        {
            foreach (var koltuk in _koltuklar)
            {
                if (koltuk.KoltukNo.ToUpper() == koltukNo.ToUpper())
                    return koltuk;
            }
            return null;
        }

        // Koltuk haritasını göster
        public void KoltukHaritasiGoster()
        {
            Console.WriteLine("\n=== KOLTUK HARİTASI ===");
            Console.WriteLine("   A   B   C   |   D   E   F");
            Console.WriteLine(new string('-', 35));

            int siraSayisi = _ucak.Kapasite / 6;
            for (int sira = 1; sira <= siraSayisi; sira++)
            {
                Console.Write($"{sira,2} ");
                for (int i = 0; i < 6; i++)
                {
                    int index = (sira - 1) * 6 + i;
                    if (index < _koltuklar.Count)
                    {
                        string sembol = _koltuklar[index].Dolu ? "[X]" : "[ ]";
                        Console.Write($"{sembol} ");
                        if (i == 2) Console.Write("| ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n[ ] = Boş, [X] = Dolu");
        }
    }
}