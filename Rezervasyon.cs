using System;

namespace UcakBiletiRezervasyonSistemi
{
    // Rezervasyon durumu enum
    public enum RezervasyonDurumu
    {
        Aktif,
        IptalEdildi
    }

    // Rezervasyon sınıfı
    public class Rezervasyon
    {
        // Private field'lar
        private static int _sayac = 1000; // PNR için sayaç
        private int _rezervasyonId;
        private string _pnr;
        private Ucus _ucus;
        private Yolcu _yolcu;
        private Koltuk _koltuk;
        private DateTime _rezervasyonTarihi;
        private RezervasyonDurumu _durum;
        private decimal _odemeTutari;

        // Public property'ler
        public int RezervasyonId
        {
            get { return _rezervasyonId; }
            set { _rezervasyonId = value; }
        }

        public string PNR
        {
            get { return _pnr; }
            set { _pnr = value; }
        }

        public Ucus Ucus
        {
            get { return _ucus; }
            set { _ucus = value; }
        }

        public Yolcu Yolcu
        {
            get { return _yolcu; }
            set { _yolcu = value; }
        }

        public Koltuk Koltuk
        {
            get { return _koltuk; }
            set { _koltuk = value; }
        }

        public DateTime RezervasyonTarihi
        {
            get { return _rezervasyonTarihi; }
            set { _rezervasyonTarihi = value; }
        }

        public RezervasyonDurumu Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        public decimal OdemeTutari
        {
            get { return _odemeTutari; }
            set { _odemeTutari = value; }
        }

        // Constructor
        public Rezervasyon(Ucus ucus, Yolcu yolcu, Koltuk koltuk)
        {
            _sayac++;
            _rezervasyonId = _sayac;
            _pnr = PNROlustur();
            _ucus = ucus;
            _yolcu = yolcu;
            _koltuk = koltuk;
            _rezervasyonTarihi = DateTime.Now;
            _durum = RezervasyonDurumu.Aktif;
            _odemeTutari = ucus.BiletFiyati;

            // Koltuğu rezerve et
            koltuk.Rezervasyon();
        }

        // Benzersiz PNR oluştur (6 karakterli)
        private string PNROlustur()
        {
            string karakterler = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] pnr = new char[6];

            for (int i = 0; i < 6; i++)
            {
                pnr[i] = karakterler[random.Next(karakterler.Length)];
            }

            return new string(pnr);
        }

        // Rezervasyonu iptal et
        public bool IptalEt()
        {
            if (_durum == RezervasyonDurumu.IptalEdildi)
            {
                return false; // Zaten iptal edilmiş
            }

            _durum = RezervasyonDurumu.IptalEdildi;
            _koltuk.Bosalt(); // Koltuğu boşalt
            return true;
        }

        // Rezervasyon bilgilerini göster
        public string RezervasyonBilgisi()
        {
            return $"PNR: {_pnr} | {_ucus.UcusNo} | {_ucus.KalkisYeri} → {_ucus.VarisYeri} | " +
                   $"{_ucus.KalkisTarihi:dd.MM.yyyy} {_ucus.KalkisSaati:hh\\:mm} | " +
                   $"Koltuk: {_koltuk.KoltukNo} | Durum: {_durum}";
        }

        // Detaylı bilet bilgisi
        public void BiletYazdir()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("                    UÇUŞ BİLETİ");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"  PNR Kodu       : {_pnr}");
            Console.WriteLine($"  Uçuş No        : {_ucus.UcusNo}");
            Console.WriteLine($"  Kalkış         : {_ucus.KalkisYeri}");
            Console.WriteLine($"  Varış          : {_ucus.VarisYeri}");
            Console.WriteLine($"  Tarih          : {_ucus.KalkisTarihi:dd.MM.yyyy}");
            Console.WriteLine($"  Saat           : {_ucus.KalkisSaati:hh\\:mm}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"  Yolcu          : {_yolcu.Ad} {_yolcu.Soyad}");
            Console.WriteLine($"  TC Kimlik No   : {_yolcu.TcNo}");
            Console.WriteLine($"  Koltuk No      : {_koltuk.KoltukNo} ({_koltuk.Tip})");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"  Ödeme Tutarı   : {_odemeTutari:C}");
            Console.WriteLine($"  Rez. Tarihi    : {_rezervasyonTarihi:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"  Durum          : {_durum}");
            Console.WriteLine(new string('=', 60));
        }
    }
}